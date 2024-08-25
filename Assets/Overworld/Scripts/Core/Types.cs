using System;

#nullable disable

namespace Overworld.Types
{
    public interface IOption<T>
    {
        T Value { get; }

        bool IsEmpty { get; }

        T GetOrDefault(T def);

        void Match(Action none = null, Action<T> some = null);

        S Match<S>(Func<S> none = null, Func<T, S> some = null);

        IOption<S> Bind<S>(Func<T, IOption<S>> f);
    }

    public interface IOptionM<T> : IOption<T>
    {
        string Message { get; }
    }

    public class None<T> : IOption<T>
    {
        public T Value
        {
            get { throw new Exception("not existed value"); }
        }

        public bool IsEmpty
        {
            get { return true; }
        }

        public T GetOrDefault(T def)
        {
            return def;
        }

        public void Match(Action none = null, Action<T> some = null)
        {
            if (none != null)
                none();
        }

        public S Match<S>(Func<S> none = null, Func<T, S> some = null)
        {
            if (none != null)
                return none();
            else
                throw new Exception("none is null");
        }

        public IOption<S> Bind<S>(Func<T, IOption<S>> f)
        {
            return Option.Return<S>();
        }
    }

    public class Some<T> : IOption<T>
    {
        public Some(T _value)
        {
            value = _value;
        }

        private T value;
        public T Value
        {
            get { return value; }
        }

        public bool IsEmpty
        {
            get { return false; }
        }

        public T GetOrDefault(T def)
        {
            return Value;
        }

        public void Match(Action none = null, Action<T> some = null)
        {
            if (some != null)
                some(Value);
        }

        public S Match<S>(Func<S> none = null, Func<T, S> some = null)
        {
            if (some != null)
                return some(Value);
            else
                throw new Exception("some is null");
        }

        public IOption<S> Bind<S>(Func<T, IOption<S>> f)
        {
            return f(Value);
        }
    }

    public sealed class NoneM<T> : None<T>, IOptionM<T>
    {
        public NoneM(string _message)
        {
            message = _message;
        }

        private string message;
        public string Message
        {
            get { return message; }
        }
    }

    public sealed class SomeM<T> : Some<T>, IOptionM<T>
    {
        public SomeM(T _value, string _message)
            : base(_value)
        {
            message = _message;
        }

        private string message;
        public string Message
        {
            get { return message; }
        }
    }

    public static class Option
    {
        public static IOption<T> Return<T>(T value)
        {
            return new Some<T>(value);
        }

        public static IOption<T> Return<T>()
        {
            return new None<T>();
        }

        public static IOptionM<T> ReturnM<T>(T value, string message)
        {
            return new SomeM<T>(value, message);
        }

        public static IOptionM<T> ReturnM<T>(T value)
        {
            return new SomeM<T>(value, string.Empty);
        }

        public static IOptionM<T> ReturnM<T>(string message)
        {
            return new NoneM<T>(message);
        }

        public static IOptionM<T> ReturnM<T>()
        {
            return new NoneM<T>(string.Empty);
        }

        public static IOption<S> Bind<T, S>(IOption<T> m, Func<T, IOption<S>> f)
        {
            return m.Bind(f);
        }
    }

    public static class OptionExtension
    {
        public static Func<T, IOption<R>> AndThen<T, S, R>(
            this Func<T, IOption<S>> f,
            Func<S, IOption<R>> g
        )
        {
            return x => f(x).Bind(g);
        }

        public static IOption<T> ToOption<T>(this T self)
        {
            return Option.Return(self);
        }

        public static IOption<S> Map<T, S>(this IOption<T> self, Func<T, S> f)
        {
            return self.Bind((elem) => Option.Return(f(elem)));
        }

        public static IOption<S> SelectMany<T, S>(this IOption<T> self, Func<T, IOption<S>> f)
        {
            return self.Bind(f);
        }

        public static IOption<R> SelectMany<T, S, R>(
            this IOption<T> self,
            Func<T, IOption<S>> f,
            Func<T, S, R> g
        )
        {
            return self.Bind((x) => f(x).Bind((y) => g(x, y).ToOption()));
        }
    }
}

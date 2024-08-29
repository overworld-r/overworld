using System.Collections.Generic;
using Overworld.Core;
using Overworld.Features.Resource.Models;
using Overworld.Models;
using Overworld.Types;

namespace Overworld.Features.Player
{
    public static class Consume
    {
        private static OverworldModel overworldModel = Simulation.GetModel<OverworldModel>();

        public static bool UseResource(List<ResourceCost> costList)
        {
            var resourcesAmount = overworldModel.PlayerStatus.resourcesAmount;
            foreach (var requireCost in costList)
            {
                return resourcesAmount
                    .Find(v => v.type == requireCost.type)
                    .Match(
                        some: havingResource =>
                        {
                            if (requireCost.amount < havingResource.amount)
                            {
                                havingResource.amount -= requireCost.amount;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        },
                        none: () => false
                    );
            }
            return false;
        }
    }
}

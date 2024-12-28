using System;
using System.Drawing;
using System.Numerics;
using ExileCore2;
using ExileCore2.Shared.Enums;
using ExileCore2.Shared.Helpers;
using ExileCore2.PoEMemory.Components;
using RectangleF = ExileCore2.Shared.RectangleF;

namespace SimpleMapNumbers
{
    public partial class SimpleMapNumbers : BaseSettingsPlugin<SimpleMapNumbersSettings>
    {
        public override bool Initialise()
        {
            return true;
        }

        public override void Render()
        {
            if (!Settings.Enable) return;

            var inventoryPanel = GameController.IngameState.IngameUi.InventoryPanel;
            if (!inventoryPanel.IsVisible) return;

            var inventorySlotItems = GameController.IngameState.ServerData.PlayerInventories[0].Inventory.InventorySlotItems;
            var stash = GameController.Game.IngameState.IngameUi.StashElement.VisibleStash;

            // Process inventory items
            foreach (var slotItem in inventorySlotItems)
            {
                if (slotItem?.Item != null)
                {
                    var mods = slotItem.Item.GetComponent<Mods>();
                    if (mods != null)
                    {
                        ProcessItem(slotItem.Item.Path, slotItem.GetClientRect(), mods.ItemRarity);
                    }
                }
            }

            // Process stash items
            if (stash != null)
            {
                foreach (var slotItem in stash.VisibleInventoryItems)
                {
                    if (slotItem?.Item != null)
                    {
                        var mods = slotItem.Item.GetComponent<Mods>();
                        if (mods != null)
                        {
                            ProcessItem(slotItem.Item.Path, slotItem.GetClientRect(), mods.ItemRarity);
                        }
                    }
                }
            }
        }

        private void ProcessItem(string itemPath, RectangleF itemRect, ItemRarity itemRarity)
        {
            for (int tier = 1; tier <= 16; tier++)
            {
                string tierString = "MapKeyTier" + tier;
                if (itemPath != null && itemPath.Contains(tierString))
                {
                    // Get tier color
                    Color tierColor;
                    if (tier <= 5)
                    {
                        tierColor = Settings.Tier1To5Color.Value;
                    }
                    else if (tier <= 10)
                    {
                        tierColor = Settings.Tier6To10Color.Value;
                    }
                    else
                    {
                        tierColor = Settings.Tier11To16Color.Value;
                    }

                    // Get frame color based on item rarity
                    Color frameColor;
                    switch (itemRarity)
                    {
                        case ItemRarity.Magic:
                            frameColor = Color.Blue;
                            break;
                        case ItemRarity.Rare:
                            frameColor = Color.Yellow;
                            break;
                        default:
                            frameColor = Color.White;
                            break;
                    }

                    if (Settings.DrawFrame)
                    {
                        Graphics.DrawFrame(itemRect, frameColor, Settings.FrameBorderSize);
                    }

                    using (Graphics.SetTextScale(Settings.TextScale.Value))
                    {
                        var topLeft = new Vector2(itemRect.TopLeft.X + 2, itemRect.TopLeft.Y + 2);
                        Graphics.DrawTextWithBackground(tier.ToString(), topLeft, tierColor, FontAlign.Left, Settings.TextBackgroundColor.Value);
                    }
                }
            }
        }
    }
}

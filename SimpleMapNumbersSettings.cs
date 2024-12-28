using ExileCore2.Shared.Attributes;
using ExileCore2.Shared.Interfaces;
using ExileCore2.Shared.Nodes;
using Newtonsoft.Json;
using System.Drawing;

namespace SimpleMapNumbers
{
    public class SimpleMapNumbersSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(true);

        [Menu("Tier 1-5 Color")]
        public ColorNode Tier1To5Color { get; set; } = new ColorNode(Color.White);

        [Menu("Tier 6-10 Color")]
        public ColorNode Tier6To10Color { get; set; } = new ColorNode(Color.Yellow);

        [Menu("Tier 11-16 Color")]
        public ColorNode Tier11To16Color { get; set; } = new ColorNode(Color.Red);

        public ToggleNode DrawFrame { get; set; } = new ToggleNode(true);
        public RangeNode<int> FrameBorderSize { get; set; } = new RangeNode<int>(1, 1, 10);
        public ColorNode TextBackgroundColor { get; set; } = new ColorNode(Color.Black);
        public RangeNode<float> TextScale { get; set; } = new RangeNode<float>(1.3f, 1.0f, 2.0f);
    }
}

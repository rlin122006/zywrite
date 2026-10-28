namespace zywrite.Core
{
    public static class CharacterMapper
    {
        // dictionary defining every supported key
        private static readonly Dictionary<char, KeyAction> Map = new()
        {
            // letter and shifted letters
            ['a'] = new((byte)Keycodes.vkA),
            ['A'] = new((byte)Keycodes.vkA, true),
            ['b'] = new((byte)Keycodes.vkB),
            ['B'] = new((byte)Keycodes.vkB, true),
            ['c'] = new((byte)Keycodes.vkC),
            ['C'] = new((byte)Keycodes.vkC, true),

            // numbers and shifted symbols
            ['1'] = new((byte)Keycodes.vk1),
            ['!'] = new((byte)Keycodes.vk1, true),
            ['2'] = new((byte)Keycodes.vk2),
            ['@'] = new((byte)Keycodes.vk2, true),

            // misc
            [' '] = new((byte)Keycodes.vkSpace)
        };

        // function to take character -> keycode and shifted status
        public static KeyAction GetAction(char character)
        {
            if (Map.TryGetValue(character, out var action))
            {
                return action;
            }

            throw new NotSupportedException($"Character '{character}' is not mapped.");
        }

        // keycode and shifted and un shifted bool
        public class KeyAction(int vkCode, bool shiftRequired = false)
        {
            public int VkCode { get; } = vkCode;
            public bool ShiftRequired { get; } = shiftRequired;
        }
    }
}

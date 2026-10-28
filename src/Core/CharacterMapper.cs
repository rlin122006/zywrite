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
            ['d'] = new((byte)Keycodes.vkD),
            ['D'] = new((byte)Keycodes.vkD, true),
            ['e'] = new((byte)Keycodes.vkE),
            ['E'] = new((byte)Keycodes.vkE, true),
            ['f'] = new((byte)Keycodes.vkF),
            ['F'] = new((byte)Keycodes.vkF, true),
            ['g'] = new((byte)Keycodes.vkG),
            ['G'] = new((byte)Keycodes.vkG, true),
            ['h'] = new((byte)Keycodes.vkH),
            ['H'] = new((byte)Keycodes.vkH, true),
            ['i'] = new((byte)Keycodes.vkI),
            ['I'] = new((byte)Keycodes.vkI, true),
            ['j'] = new((byte)Keycodes.vkJ),
            ['J'] = new((byte)Keycodes.vkJ, true),
            ['k'] = new((byte)Keycodes.vkK),
            ['K'] = new((byte)Keycodes.vkK, true),
            ['l'] = new((byte)Keycodes.vkL),
            ['L'] = new((byte)Keycodes.vkL, true),
            ['m'] = new((byte)Keycodes.vkM),
            ['M'] = new((byte)Keycodes.vkM, true),
            ['n'] = new((byte)Keycodes.vkN),
            ['N'] = new((byte)Keycodes.vkN, true),
            ['o'] = new((byte)Keycodes.vkO),
            ['O'] = new((byte)Keycodes.vkO, true),
            ['p'] = new((byte)Keycodes.vkP),
            ['P'] = new((byte)Keycodes.vkP, true),
            ['q'] = new((byte)Keycodes.vkQ),
            ['Q'] = new((byte)Keycodes.vkQ, true),
            ['r'] = new((byte)Keycodes.vkR),
            ['R'] = new((byte)Keycodes.vkR, true),
            ['s'] = new((byte)Keycodes.vkS),
            ['S'] = new((byte)Keycodes.vkS, true),
            ['t'] = new((byte)Keycodes.vkT),
            ['T'] = new((byte)Keycodes.vkT, true),
            ['u'] = new((byte)Keycodes.vkU),
            ['U'] = new((byte)Keycodes.vkU, true),
            ['v'] = new((byte)Keycodes.vkV),
            ['V'] = new((byte)Keycodes.vkV, true),
            ['w'] = new((byte)Keycodes.vkW),
            ['W'] = new((byte)Keycodes.vkW, true),
            ['x'] = new((byte)Keycodes.vkX),
            ['X'] = new((byte)Keycodes.vkX, true),
            ['y'] = new((byte)Keycodes.vkY),
            ['Y'] = new((byte)Keycodes.vkY, true),
            ['z'] = new((byte)Keycodes.vkZ),
            ['Z'] = new((byte)Keycodes.vkZ, true),

            // numbers and shifted symbols
            ['1'] = new((byte)Keycodes.vk1),
            ['!'] = new((byte)Keycodes.vk1, true),
            ['2'] = new((byte)Keycodes.vk2),
            ['@'] = new((byte)Keycodes.vk2, true),
            ['3'] = new((byte)Keycodes.vk3),
            ['#'] = new((byte)Keycodes.vk3, true),
            ['4'] = new((byte)Keycodes.vk4),
            ['$'] = new((byte)Keycodes.vk4, true),
            ['5'] = new((byte)Keycodes.vk5),
            ['%'] = new((byte)Keycodes.vk5, true),
            ['6'] = new((byte)Keycodes.vk6),
            ['^'] = new((byte)Keycodes.vk6, true),
            ['7'] = new((byte)Keycodes.vk7),
            ['&'] = new((byte)Keycodes.vk7, true),
            ['8'] = new((byte)Keycodes.vk8),
            ['*'] = new((byte)Keycodes.vk8, true),
            ['9'] = new((byte)Keycodes.vk9),
            ['('] = new((byte)Keycodes.vk9, true),
            ['0'] = new((byte)Keycodes.vk0),
            [')'] = new((byte)Keycodes.vk0, true),

            // punctuation and symbols
            [','] = new((byte)Keycodes.vkComma),
            ['<'] = new((byte)Keycodes.vkComma, true),
            ['.'] = new((byte)Keycodes.vkPeriod),
            ['>'] = new((byte)Keycodes.vkPeriod, true),
            ['/'] = new((byte)Keycodes.vkSlash),
            ['?'] = new((byte)Keycodes.vkSlash, true),
            [';'] = new((byte)Keycodes.vkSemicolon),
            [':'] = new((byte)Keycodes.vkSemicolon, true),
            ['\''] = new((byte)Keycodes.vkQuote),
            ['"'] = new((byte)Keycodes.vkQuote, true),
            ['['] = new((byte)Keycodes.vkBracketOpen),
            ['{'] = new((byte)Keycodes.vkBracketOpen, true),
            [']'] = new((byte)Keycodes.vkBracketClose),
            ['}'] = new((byte)Keycodes.vkBracketClose, true),
            ['\\'] = new((byte)Keycodes.vkBackslash),
            ['|'] = new((byte)Keycodes.vkBackslash, true),
            ['`'] = new((byte)Keycodes.vkTilde),
            ['~'] = new((byte)Keycodes.vkTilde, true),
            ['-'] = new((byte)Keycodes.vkMinus),
            ['_'] = new((byte)Keycodes.vkMinus, true),

            // equals and plus share one physical key (0xbb is named vkPlus after its shifted form)
            ['='] = new((byte)Keycodes.vkPlus),
            ['+'] = new((byte)Keycodes.vkPlus, true),

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

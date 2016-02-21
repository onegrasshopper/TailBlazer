using System;
using System.Collections.Generic;
using System.Linq;
using DynamicData;
using DynamicData.Kernel;
using MaterialDesignThemes.Wpf;
using TailBlazer.Domain.FileHandling.Search;
using TailBlazer.Domain.Formatting;

namespace TailBlazer.Views.Formatting
{
    //public static class KnownTerms
    //{
    //    publ
    //}

    public sealed class ColourSelector : IColourSelector
    {
        private readonly IColourProvider _colourProvider;
        private Dictionary<HueKey, Hue> _hues;
        private readonly Hue _defaultHighlight;
        private readonly DefaultHue[] _defaults;

        public ColourSelector(IColourProvider colourProvider)
        {
            _colourProvider = colourProvider;

            _hues = colourProvider.Hues.ToDictionary(h => h.Key);
            _defaultHighlight = _colourProvider.Hues
                                    .Last(s => s.Swatch.Equals("amber", StringComparison.OrdinalIgnoreCase));

            _defaults = Load().ToArray();
        }

      

        public Hue SelectFor(string text)
        {
            var match = _defaults
                .FirstOrDefault(hue => hue.MatchTextOnCase
                    ? hue.Text.Equals(text)
                    : hue.Text.Equals(text, StringComparison.OrdinalIgnoreCase));

            return match != null ? match.Hue :  _defaultHighlight;
        }

        private IEnumerable<DefaultHue> Load()
        {
            yield return new DefaultHue("DEBUG", Lookup("blue", "Accent400"));
            yield return new DefaultHue("INFO", Lookup("yellow", "Accent400"));
            yield return new DefaultHue("WARN", Lookup("orange", "Accent400"));
            yield return new DefaultHue("WARNING", Lookup("orange", "Accent400"));
            yield return new DefaultHue("ERROR", Lookup("red", "Accent200"));

        }

        private Hue Lookup(string swatch, string name)
        {
            return _hues.Lookup(new HueKey(swatch, name))
                .ValueOrThrow(() => new MissingKeyException(swatch + "."+ name + " is invalid"));
        }

        private class DefaultHue
        {
            public string Text { get; }
            public Hue Hue { get; }
            public bool MatchTextOnCase { get; }


            public DefaultHue(string text, Hue hue, bool matchTextOnCase = true)
            {
                Text = text;
                Hue = hue;
                MatchTextOnCase = matchTextOnCase;
            }

        }
    }
}
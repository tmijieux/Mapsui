using Mapsui.Providers;
using Mapsui.Styles;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace Mapsui.Rendering.Skia
{
    public class SkiaTarget
    {
        public IDictionary<int, Tuple<IFeature, IStyle>> _featureStyle = new Dictionary<int, Tuple<IFeature, IStyle>>();
        public SKCanvas Canvas { get; set; }
        public bool InfoMode { get; set; } = true;
        // todo: use something bigger than byte
        byte _currentId = 1; 
        IDictionary<int, int> _randomizer = new Dictionary<int, int>();
        Random _random = new Random(555);

        public SkiaTarget()
        {
            _randomizer = CreateRandomDictionary();
        }

        private IDictionary<int, int> CreateRandomDictionary()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();

            List<int> list = new List<int>();
            for (int i = 0; i < 256; i++)
            {
                list.Add(i);
            }
            Shuffle(list);
            for (int i = 0; i < 256; i++)
            {
                dictionary[i] = list[i];
            }
            return dictionary;
        }

        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public SKColor GetInfoColor()
        {            
            var randomNumber = _randomizer[_currentId];
            return new SKColor(0, 0, (byte)randomNumber);
        }

        public (IFeature, IStyle) GetFeatureStyle(int featureStyleId)
        {
            if (!_featureStyle.ContainsKey(featureStyleId)) return (null, null);

            return _featureStyle[featureStyleId].ToValueTuple();
        }

        public void AddFeatureStyle(IFeature feature, IStyle style)
        {
            _featureStyle[_randomizer[_currentId]] = new Tuple<IFeature, IStyle>(feature, style);
            _currentId++;
        }
    }
}

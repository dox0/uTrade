using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace uTrade.Controls
{
    public enum DrawObjectType
    {
        Line,
        zVLines,
        CandleLine,
        vLines
    }
    public class DrawObject
    {
        public Color Color;
        public double Thickness;
        public string Name;

        public DrawObjectType Type;

        public double[] Vals;
        public double[] Vals1;
        public double[] Vals2;
        public double[] Vals3;

        public string drawItemHandler = null;
    }
    public class DrawingObject
    {
        ConcurrentDictionary<string, DrawObject> Objects = new ConcurrentDictionary<string, DrawObject>();

        internal ConcurrentDictionary<string, DrawObject> All()
        {
            return Objects;
        }

        public void Add(List<DrawObject> lstDrawObj)
        {
            foreach (DrawObject oDrawObj in lstDrawObj)
            {
                Objects.TryAdd(oDrawObj.Name, oDrawObj);
            }
        }

        public void Clear()
        {
            Objects.Clear();
        }

        public void setDrawItemEventHandler(string id, string handlerName)
        {
            if (Objects.ContainsKey(id))
            {
                Objects[id].drawItemHandler = handlerName;
            }
        }
    }
}

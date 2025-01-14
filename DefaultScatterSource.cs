using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plottist
{
    public class DefaultScatterSource : IScatterSource
    {
        public int MinRenderIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MaxRenderIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public AxisLimits GetLimits()
        {
            throw new NotImplementedException();
        }

        public CoordinateRange GetLimitsX()
        {
            throw new NotImplementedException();
        }

        public CoordinateRange GetLimitsY()
        {
            throw new NotImplementedException();
        }

        public DataPoint GetNearest(Coordinates location, RenderDetails renderInfo, float maxDistance = 15)
        {
            throw new NotImplementedException();
        }

        public DataPoint GetNearestX(Coordinates location, RenderDetails renderInfo, float maxDistance = 15)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Coordinates> GetScatterPoints()
        {
            throw new NotImplementedException();
        }
    }
}

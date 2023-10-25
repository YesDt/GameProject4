using GameProject4.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject4
{
    public class obstacle
    {
        private BoundingRectangle _bounds;

        public BoundingRectangle Bounds => _bounds;

        public obstacle(BoundingRectangle bounds)
        {
            _bounds = bounds;
        }
    }
}

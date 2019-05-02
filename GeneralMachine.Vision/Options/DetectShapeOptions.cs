using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Vision.Analysis;
using NationalInstruments.Vision;

namespace GeneralMachine.Vision
{
    public class DetectShapeOptions: IOptions
    {
        public double MinimumMatchScore { get; set; } = 600;
  
        public GeometricMatchModes Mode { get; set; } = GeometricMatchModes.OcclusionInvariant;

        public double MinScale { get; set; } = 50;

        public double MaxScale { get; set; } = 50;

        public double MinRotation { get; set; } = -20;

        public double MaxRotation { get; set; } = 20;

        public ShapeDetectionOptions Options
        {
            get
            {
                var op = new ShapeDetectionOptions(this.Mode, this.MinimumMatchScore);
                op.RotationAngleRanges.Add(new Range(this.MinRotation, this.MaxRotation));
                op.ScaleRange.Minimum = this.MinScale;
                op.ScaleRange.Maximum = this.MaxScale;
                return op;
            }
        }
    }
}

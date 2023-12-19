﻿using RealisticDensity.Common;

namespace RealisticDensity.Systems
{
    public static class WorkforceFactors
    {
        public static Factor4 Commercial = new(1.0f, 1.4f, 1.7f, 2f);
        public static Factor4 Office = new(1.0f, 1.50f, 2.0f, 3.0f);
        public static Factor4 IndustryExtractor = new(1.0f, 1.3f, 1.5f, 1.7f);
        public static Factor4 IndustrySelling = new(1.0f, 1.4f, 1.7f, 2.0f);
        public static Factor4 IndustryProcessing = new(1.0f, 1.4f, 1.7f, 2.0f);
        public static Factor4 PowerPlant = new(1.0f, 4f, 8f, 10f);
        public static Factor4 School = new(1.0f, 3f, 5f, 7f);
        public static Factor4 Hospital = new(1.0f, 2f, 4f, 6f);
        public static Factor4 PoliceStation = new(1.0f, 1.5f, 2f, 4f);
        public static Factor4 FireStation = new(1.0f, 1.5f, 2f, 4f);
        public static Factor4 CargoTransportStation = new(1.0f, 2f, 3f, 4f);
    }
}
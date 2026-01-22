namespace Master.Entity
{
    public class LocalNetwork
    {
        public const string Secret = "673b29118d844738a958a38e8580bdaaf8e641e2ca2c451e973d3dae215f84d27ac5b43f0edf43f5829126253b05d0abbaf21dd3d50b417aa526ab582aa5df6ea53b6339d49b49808ab916567977ecc3";
        public const string Issuer = "Issuer";
        public const string Audience = "Audience";

        public string database { get; set; }        
        public int maxCores { get; set; }
        public string apiRouter { get; set; }
        public string cacheLocation { get; set; }
    }
}

namespace Springboard365.UnitTest.Xrm.Core
{
    using Springboard365.UnitTest.Core;

    public abstract class Specification<TUnderTest> : SpecificationBase where TUnderTest : class
    {
        public SpecificationFixture<TUnderTest> TestFixture { get; set; }
    }
}
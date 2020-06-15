namespace Springboard365.UnitTest.Xrm.Core
{
    using System;
    using Moq;
    using Springboard365.UnitTest.Core;

    public abstract class SpecificationFixture<TUnderTest> : SpecificationFixtureBase<TUnderTest>
        where TUnderTest : class
    {
        protected SpecificationFixture()
        {
            ServiceProvider = ServiceProviderInitializer.Setup().WithDefault();
        }

        public Mock<IServiceProvider> ServiceProvider { get; set; }
    }
}
namespace Springboard365.UnitTest.Xrm.Core
{
    using System;
    using Microsoft.Xrm.Sdk;
    using Moq;

    public static class ServiceProviderExtensions
    {
        public static Mock<IServiceProvider> WithInputParameters(this Mock<IServiceProvider> serviceProviderMock, Func<ParameterCollection> parameterCollection)
        {
            var mockPluginContext = new Mock<IPluginExecutionContext>();

            mockPluginContext
                .Setup(context => context.InputParameters)
                .Returns(parameterCollection);

            serviceProviderMock
                .Setup(provider => provider.GetService(typeof(IPluginExecutionContext)))
                .Returns(mockPluginContext.Object);

            return serviceProviderMock;
        }

        public static Mock<IServiceProvider> WithPreEntityImages(this Mock<IServiceProvider> serviceProviderMock, Func<EntityImageCollection> entityImageCollection)
        {
            var mockPluginContext = new Mock<IPluginExecutionContext>();

            mockPluginContext
                .Setup(context => context.PreEntityImages)
                .Returns(entityImageCollection);

            serviceProviderMock
                .Setup(provider => provider.GetService(typeof(IPluginExecutionContext)))
                .Returns(mockPluginContext.Object);

            return serviceProviderMock;
        }

        public static Mock<IServiceProvider> WithPostEntityImages(this Mock<IServiceProvider> serviceProviderMock, Func<EntityImageCollection> entityImageCollection)
        {
            var mockPluginContext = new Mock<IPluginExecutionContext>();

            mockPluginContext
                .Setup(context => context.PostEntityImages)
                .Returns(entityImageCollection);

            serviceProviderMock
                .Setup(provider => provider.GetService(typeof(IPluginExecutionContext)))
                .Returns(mockPluginContext.Object);

            return serviceProviderMock;
        }

        public static Mock<IServiceProvider> WithDefault(this Mock<IServiceProvider> serviceProviderMock)
        {
            var mockPluginContext = new Mock<IPluginExecutionContext>().WithDefault();

            mockPluginContext
                .Setup(context => context.InputParameters)
                .Returns(new ParameterCollection { { "Target", new Entity("contact") { Id = Guid.NewGuid() } } });

            mockPluginContext
                .Setup(context => context.PreEntityImages)
                .Returns(new EntityImageCollection { { "PreImage", new Entity("contact") { Id = Guid.NewGuid() } } });

            mockPluginContext
                .Setup(context => context.PostEntityImages)
                .Returns(new EntityImageCollection { { "PostImage", new Entity("contact") { Id = Guid.NewGuid() } } });

            serviceProviderMock
                .Setup(provider => provider.GetService(typeof(IPluginExecutionContext)))
                .Returns(mockPluginContext.Object);

            return serviceProviderMock;
        }
    }
}
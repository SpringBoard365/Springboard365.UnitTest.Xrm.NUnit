namespace Springboard365.UnitTest.Xrm.Core
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Messages;
    using Microsoft.Xrm.Sdk.Metadata;
    using Moq;

    public static class ServiceProviderInitializer
    {
        public static Mock<IServiceProvider> Setup()
        {
            var mockPluginContext = SetupPluginContext();
            var mockOrganizationServiceFactory = SetupOrganizationServiceFactory();
            var mockTracingService = new Mock<ITracingService>();

            return SetupServiceProvider(mockPluginContext, mockOrganizationServiceFactory, mockTracingService);
        }

        public static Mock<IOrganizationServiceFactory> SetupOrganizationServiceFactory()
        {
            var mockOrganizationService = SetupOrganizationService();
            var mockOrganizationServiceFactory = new Mock<IOrganizationServiceFactory>();

            mockOrganizationServiceFactory
                .Setup(factory => factory.CreateOrganizationService(It.IsAny<Guid?>()))
                .Returns(mockOrganizationService.Object);

            return mockOrganizationServiceFactory;
        }

        public static Mock<IPluginExecutionContext> SetupPluginContext()
        {
            var mockPluginContext = new Mock<IPluginExecutionContext>();
            mockPluginContext
                .Setup(context => context.UserId)
                .Returns(Guid.NewGuid);

            return mockPluginContext;
        }

        private static Mock<IServiceProvider> SetupServiceProvider(Mock<IPluginExecutionContext> mockPluginContext, Mock<IOrganizationServiceFactory> mockOrganizationServiceFactory, Mock<ITracingService> mockTracingService)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(provider => provider.GetService(typeof(IPluginExecutionContext)))
                           .Returns(mockPluginContext.Object);
            serviceProvider.Setup(provider => provider.GetService(typeof(IOrganizationServiceFactory)))
                           .Returns(mockOrganizationServiceFactory.Object);
            serviceProvider.Setup(provider => provider.GetService(typeof(ITracingService))).Returns(mockTracingService.Object);

            return serviceProvider;
        }

        private static Mock<IOrganizationService> SetupOrganizationService()
        {
            var mockOrganizationService = new Mock<IOrganizationService>();

            mockOrganizationService.Setup(service => service.Execute(It.IsAny<RetrieveAttributeRequest>()))
                                   .Returns(GetDummy());

            return mockOrganizationService;
        }

        private static RetrieveAttributeResponse GetDummy()
        {
            return new RetrieveAttributeResponse
            {
                Results = new ParameterCollection
                {
                    new KeyValuePair<string, object>("AttributeMetadata", GetDummyStatusAttributeMetadata()),
                }
            };
        }

        private static StatusAttributeMetadata GetDummyStatusAttributeMetadata()
        {
            var optionMetadataList = new List<OptionMetadata>
            {
                new StatusOptionMetadata(1, 1)
            };

            return new StatusAttributeMetadata
            {
                OptionSet = new OptionSetMetadata(new OptionMetadataCollection(optionMetadataList))
            };
        }
    }
}
using System.ServiceModel;

namespace Coffee.RemoteService
{
    using System;

    [ServiceContract]
    public interface IHelpService
    {
        [OperationContract(IsOneWay = true)]
        void CreateAssociation(Guid userId);
    }
}

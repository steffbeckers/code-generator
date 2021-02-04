using System;

namespace CodeGenOutput.API.BLL
{
    public interface IBusinessLogicLayer :
        IAccountBLL,
        IContactBLL
    {}

    public class BusinessLogicLayer : IBusinessLogicLayer
    {
    }
}

using System.Threading.Tasks;
using PLX.API.Data.DTO;

namespace PLX.API.Services
{
    public class BaseService
    {
        public ApiOkResponse OkResponse(object data, string resultCode = "11003", object[] arguments = null)
        {
            return new ApiOkResponse(data, resultCode, arguments);

        }
        public ApiErrorResponse ErrorResponse(string resultCode, object[] arguments = null)
        {
            return new ApiErrorResponse(resultCode, arguments);

        }
    }

}

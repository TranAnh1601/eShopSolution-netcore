using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<PagedResult<UserVm>> GetUsersPagings(GetUserPagingRequest request);

        Task<bool> RegisterUser(RegisterRequest registerRequest);

        //Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);

        //Task<ApiResult<UserVm>> GetById(Guid id);

        //Task<ApiResult<bool>> Delete(Guid id);

        //Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);

    }
}

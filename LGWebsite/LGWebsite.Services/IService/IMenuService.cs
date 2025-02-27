using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AODWebsite.Services.IService
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllMenuAsync();
        Task<MenuCreateViewModel> MenuCreateViewModelAsync();
        Task<bool> CreateMenuAsync(MenuCreateViewModel data);
        Task<MenuCreateViewModel> MenuEditAsync(int id);
        Task UpdateMenuAsync(MenuCreateViewModel model, int id);
        Task DeleteMenuAsync(int id);
    }
}

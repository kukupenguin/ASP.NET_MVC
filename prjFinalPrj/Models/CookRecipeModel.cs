using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace prjFinalPrj.Models
{
    public class CookRecipeModel
    {
        public List<TableCooks1081655> Cook { get; set; }
        public IPagedList<TableRecipes1081655> Recipe { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPW217_PortfolioProject2021.Models
{
    public class ThreeDModel
    {
        public int ThreeDModelId { get; set; }

        public string Title { get; set; }

        // Add relationship to the UserAccount owner
        public ApplicationUser Owner { get; set; }

    }
}

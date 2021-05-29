using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastTripApp.DAO.Repository
{
    public class RepositoryReview : RepositoryGeneric<Review>, IRepositoryReview
    {
        private readonly UsingIdentityContext _сontext;
        public RepositoryReview(UsingIdentityContext usingIdentityContext) : base(usingIdentityContext)
        {
            _сontext = usingIdentityContext;
        }

        public List<Review> GetWithInclude()
        {
            return _сontext.Reviews.Include(p => p.Comments).ThenInclude(p=>p.User).Include(p => p.User).ToList();
        }


        public Review GetByIdWithInclude(int id)
        {
            var list = GetWithInclude();
            return list.FirstOrDefault(p=>p.ReviewId == id);
        }
    }
}


//@section scripts
//{
//    <script type="text/javascript">
//        function getInfo(id) {
//            $.ajaxSetup({ cache: false });

//            $.get('Trip/Details?id=' + id, function(data) {
//                $('#dialogContent').html(data);
//                $('#modDialog').modal('show');
//        });
//    }
//    </script>
//}
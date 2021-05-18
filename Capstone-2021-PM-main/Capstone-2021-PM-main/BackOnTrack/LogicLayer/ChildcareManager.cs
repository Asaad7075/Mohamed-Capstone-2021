using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;

namespace LogicLayer
{
    public class ChildcareManager : IChildcareManager
    {
        private IChildcareAccessor _childcareAccessor = null;

        /// <summary>
        /// Chase martin
        /// Created: 2021/03/4
        /// 
        /// Empty constructor that holds the ChildcareAccessor
        /// </summary>
        public ChildcareManager()
        {
            _childcareAccessor = new ChildcareAccessor();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/3
        /// 
        /// Dependency Inversion
        /// </summary>
        /// <param name="dataAccessor"></param>
        public ChildcareManager(IChildcareAccessor dataAccessor)
        {
            _childcareAccessor = dataAccessor;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/3
        /// 
        /// Calls the RetrieveAllChildcareTypes() method
        /// from the DataAccessLayer to display all
        /// types.
        /// </summary>
        /// <returns></returns>
        public List<Childcare> RetrieveAllChildcareTypes()
        {
            List<Childcare> types = null;

            try
            {
                types = _childcareAccessor.SelectAllChildcareTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return types;
        }

    }
}


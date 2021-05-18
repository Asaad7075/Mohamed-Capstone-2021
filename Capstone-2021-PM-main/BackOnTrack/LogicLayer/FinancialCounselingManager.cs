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
    public class FinancialCounselingManager : IFinancialCounselingManager
    {
        private IFinancialCounselingAccessor _financialCounselingAccessor = null;

        /// <summary>
        /// Chase martin
        /// Created: 2021/03/09
        /// 
        /// Empty constructor that holds the FinancialCounselingAccessor
        /// </summary>
        public FinancialCounselingManager()
        {
            _financialCounselingAccessor = new FinancialCounselingAccessor();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/09
        /// 
        /// Dependency Inversion
        /// </summary>
        /// <param name="dataAccessor"></param>
        public FinancialCounselingManager(IFinancialCounselingAccessor dataAccessor)
        {
            _financialCounselingAccessor = dataAccessor;
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/09
        /// 
        /// Calls the RetrieveAllCounselingTypes() method
        /// from the DataAccessLayer to display all
        /// types.
        /// </summary>
        /// <returns></returns>
        public List<FinancialCounseling> RetrieveAllCounselingTypes()
        {
            List<FinancialCounseling> types = null;

            try
            {
                types = _financialCounselingAccessor.SelectAllCounselingTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return types;
        }

    }
}



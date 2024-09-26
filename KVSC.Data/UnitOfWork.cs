

using KVSC.Data.Models;
using KVSC.Data.Repository;

namespace KVSC.Data
{
    public class UnitOfWork
    {
        private FA24_SE1720_231_G5_KVSCContext _context;
        private CustomerRepository _customerRepository;
        private ServiceRepository _serviceRepository;
        private ServiceCategoryRepository _categoryRepository;
        private ServiceRequestRepository _serviceRequestRepository;
        private DoctorRepository _doctorRepository;
        private DoctorSheduleRepository _doctorSheduleRepository;
        public UnitOfWork()
        {

            _context ??= new FA24_SE1720_231_G5_KVSCContext();

        }

        public CustomerRepository CustomerRepository
        {
            get { return _customerRepository ??= new CustomerRepository(_context); }
        }
        public ServiceRequestRepository ServiceRequestRepository
        {
            get { return _serviceRequestRepository ??= new ServiceRequestRepository(_context); }
        }

        public DoctorRepository DoctorRepository
        {
            get { return _doctorRepository ??= new DoctorRepository(_context); }
        }

        public DoctorSheduleRepository DoctorSheduleRepository
        {
            get { return _doctorSheduleRepository ??= new DoctorSheduleRepository(_context); }
        }

        public ServiceCategoryRepository serviceCategoryRepository
        {
            get
            {
                return _categoryRepository ??= new Repository.ServiceCategoryRepository(_context);
            }

        }

        public ServiceRepository serviceRepository
        {
            get
            {
                return _serviceRepository ??= new Repository.ServiceRepository(_context);
            }
        }

        ////TO-DO CODE HERE/////////////////

            #region Set transaction isolation levels

            /*
            Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

            Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

            Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

            Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

            Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
             */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion

    }
}

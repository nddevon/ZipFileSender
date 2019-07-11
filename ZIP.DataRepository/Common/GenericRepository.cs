using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIP.DataRepository.Common {
    public class GenericRepository {
        private DbContext _context;

        public GenericRepository(DbContext context) {
            _context = context;
        }

        public T GetById<T>(object id) where T : class {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> Query<T>() where T : class {
            return _context.Set<T>();
        }

        public void Insert<T>(T entity) where T : class {
            _context.Set<T>().Add(entity);
        }

        public bool SaveChenge() {
            _context.SaveChanges();
            return true;
        }
    }
}

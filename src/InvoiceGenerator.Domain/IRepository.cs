using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Domain
{
    public interface IRepository<T> where T : class
    {
        int Add(T item);
        List<T> GetAll();
    }
}

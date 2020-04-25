﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtilities.ObjectPool
{
    public interface IPoolable
    {
        void OnGet();

        void OnRecycle();
    }

    public interface TPoolAllocator<T>
    {
        T CreateNew();
    }
}

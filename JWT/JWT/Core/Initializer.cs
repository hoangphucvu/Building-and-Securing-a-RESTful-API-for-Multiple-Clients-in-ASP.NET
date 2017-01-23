using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JWT.Core
{
    public class Initializer : MigrateDatabaseToLatestVersion<BooksContext, Configuration>
    {
    }
}
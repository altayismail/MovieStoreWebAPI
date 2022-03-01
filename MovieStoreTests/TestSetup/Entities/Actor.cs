using MovieStoreWebApp.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreTest.TestSetup.Entities
{
    public static class Actor
    {
        public static void AddActor(this MovieStoreDbContext context)
        {
            context.Actors.AddRange
            (
                new MovieStoreWebApp.Entities.Actor { Name = "İsmail",Surname = "Altay"},
                new MovieStoreWebApp.Entities.Actor { Name = "Ugurcan", Surname = "Cakır"},
                new MovieStoreWebApp.Entities.Actor { Name = "Marek", Surname = "Hamsik"}
            );
        }
    }
}

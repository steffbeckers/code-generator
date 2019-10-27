using Test.API.Models;

namespace Test.API.DAL.Repositories
{
    public class NoteRepository : Repository<Note>
    {
        private new readonly TestContext context;

        public NoteRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}

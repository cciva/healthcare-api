using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MedCenter
{
    // V2 improvements :
    // added request handler that will deal with data storage - in this case
    // there should be repository or storage interface with purpose of storing
    // or fetching data from db
    // for the sake of simplicity, illustration of such interface is given
    // below and is commented
    public class CreateTherapyHandler : IRequestHandler<CreateTherapyCommand, Therapy>
    {
        //private readonly IStorage<Therapy> _store;

        // public CreateTherapyHandler(IStorage<Therapy> store)
        // {
        //     _store = store;
        // }

        public Task<Therapy> Handle(CreateTherapyCommand rq, CancellationToken cancel)
        {
            // return await _store.Create<Therapy>(rq, cancel);
            return Task.FromResult(new Therapy());
        }
    }
}
﻿using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.Models;
using CodeGenOutput.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class GetAccounts : IRequest<Response<List<AccountListVM>>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
    }

    public class GetAccountsHandler : IRequestHandler<GetAccounts, Response<List<AccountListVM>>>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public GetAccountsHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<List<AccountListVM>>> Handle(GetAccounts request, CancellationToken cancellationToken)
        {
            Response<List<AccountListVM>> response = new Response<List<AccountListVM>>();

            List<Account> accounts = (await _bll.GetAccountsAsync()).ToList();
            response.Data = _mapper.Map<List<AccountListVM>>(accounts);

            return response;
        }
    }
}

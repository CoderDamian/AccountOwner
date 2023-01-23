using AutoMapper;
using Entities.HateOas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace AccountOwnerServer.Seedwork
{
    public class RestControllerBase : ControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;
        private readonly IMapper _mapper;

        public RestControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollection, IMapper mapper)
        {
            this._routes = actionDescriptorCollection.ActionDescriptors.Items;
            this._mapper = mapper;
        }

        internal Link URLLink(string relation, string routeName, object values)
        {
            var route = _routes.FirstOrDefault(f => f.AttributeRouteInfo.Name.Equals(routeName));

            var method=_routes.ActionConstraints.OfType<HttpMethodActionConstraint>().First().HttpMethods.First();

            var url = Url.Link(routeName, values).ToLower();
            
            return new Link(url, relation, method);
        }
    }
}

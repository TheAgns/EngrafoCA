using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;

namespace Domain.ValueObjects
{
    public class DocumentationCategory : ValueObject
    {
        public string Name { get; init; }
        public bool Hide { get; init; }
        public bool Inactive { get; init; }

        public IList<Documentation> Documentations { get; set; }

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Name; 
            yield return Hide;
            yield return Inactive;
		}
	}
}

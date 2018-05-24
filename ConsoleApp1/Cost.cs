using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public class Cost
	{
		double _price = 3.5;
		public virtual double GetCost()
		{
			return _price;
		}

		public virtual string GetDescription()
		{
			return "price";
		}
	}

	public class CostAddition : Cost
	{
		protected double _price = 0;
		protected string _desc = "generic";
		protected Cost _baseCost = null;

		public CostAddition(Cost baseC)
		{
			_baseCost = baseC;
		}

		public override double GetCost()
		{
			return _baseCost.GetCost() + _price;
		}

		public override string GetDescription()
		{
			return string.Format("{0}, {1}", _baseCost.GetDescription(), _desc);
		}
	}

	public class Exchange : CostAddition
	{
		double _rate = 4.2;


		public Exchange(Cost baseC) : base(baseC)
		{
			_desc = "exchange";
		}

		public override double GetCost()
		{			
			return _baseCost.GetCost() * _rate;
		}
	}

	public class X : CostAddition
	{


		public X(Cost baseC) : base(baseC)
		{
			_desc = "other";
			_price = 5;
		}
	}
}

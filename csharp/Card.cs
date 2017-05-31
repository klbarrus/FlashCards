using System;
using System.Collections .Generic ;
using System.Text;

namespace FlashCard
{
	public class Definition
	{
		public string Grammar
		{
			set;get;
		}
		
		public string Meaning
		{
			set;get;
		}
	}
	
	public class Card
	{
		List<Definition> def;
		
		public Card ()
		{
			def = new List<Definition>();
		}
		
		public string Character
		{
			set;get;
		}
		
		public string Pinyin
		{
			set; get;
		}
		
		public int NumDefinitions
		{
			get
			{
				return def.Count ;
			}
		}
		
		public Definition getDefinition(int n)
		{
			if ( 0 <= n && n <= NumDefinitions -1)
			{
				return def[n];
			}
			
			return null;
		}
		
		public void addDefinition(string grammar, string meaning)
		{
			Definition d = new Definition ();
			d.Grammar = grammar;
			d.Meaning = meaning;
			def.Add (d);
		}
	}
}


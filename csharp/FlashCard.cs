using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace FlashCard
{
	public class FlashCardApp : Form
	{
		Button btnPrev, btnNext;
		int cxBtn, cyBtn, dxBtn;
		Font fChar, fText, fInfo;
		bool bShowCardFront;
		int numDataFiles;
		List<Card> cardList;
		int nCurrentCard;
		
		public static void Main(string[] args)
		{
			if (args.Length >= 1)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new FlashCardApp(args));
			}
		}
		
		public FlashCardApp (string[] args)
		{
			Text = "Flash Card";
			ResizeRedraw = true;
			
			PlatformID platform = System.Environment.OSVersion.Platform;
			
			if (platform == PlatformID.Unix)
			{
				// unfortunately OSX shows up as Unix not MacOSX
				OperatingSystem os = System.Environment.OSVersion;
				
				// linux shows as Build # Major # Minor #
				// macosx shows as Build 0 Major # Minor #
				// so if Build is non-zero, take that as linux
				//    if Build is zero, take that as macosx
				
				if (os.Version.Build == 0)
				{
					fChar = new Font("Apple LiSung", 24);
				}
				else
				{
					fChar = new Font("AR PL UMing CN", 24);
				}
			}
			else
			{
				fChar = new Font("MingLiU", 24);
			}
						
			fText = new Font("Courier New", 12);
			fInfo = new Font("Courier New", 8);
			
			cxBtn = 5 * Font.Height;
			cyBtn = 2 * Font.Height;
			dxBtn = Font.Height;
			
			btnPrev = new Button();
			btnPrev.Parent = this;
			btnPrev.Text = "&<";
			btnPrev.Size = new Size(cxBtn, cyBtn);
			btnPrev.Click += new EventHandler(btnPrev_Click);
			
			btnNext = new Button();
			btnNext.Parent = this;
			btnNext.Text = "&>";
			btnNext.Size = new Size(cxBtn, cyBtn);
			btnNext.Click += new EventHandler(btnNext_Click);
			
			btnPrev.TabIndex = 1;
			btnNext.TabIndex = 0;
			
			Paint += new PaintEventHandler(FlashCardApp_Paint);
			
			numDataFiles = args.Length;
			readFlashCards(args);
			shuffleCards();
			bShowCardFront = true;
			nCurrentCard = 0;
			
			OnResize(EventArgs.Empty);
		}
			
		void btnPrev_Click(object sender, EventArgs e)
		{
			if (bShowCardFront == false)
			{
				bShowCardFront = true;
			}
			else
			{
				if (nCurrentCard != 0)
				{
					--nCurrentCard;
					bShowCardFront = true;
				}
			}
				
			Invalidate();
		}
			
		void btnNext_Click(object sender, EventArgs e)
		{
			if (nCurrentCard == cardList.Count - 1 && bShowCardFront == false)
			{
			}
			else
			{
				if (bShowCardFront == true)
				{
					bShowCardFront = false;
				}
				else
				{
					bShowCardFront = true;
					++nCurrentCard;
				}
			}
			
			Invalidate();
		}
		
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			
			btnPrev.Location = new Point( (ClientSize.Width * 2 / 5) - cxBtn - dxBtn / 2,
			                              (ClientSize.Height - cyBtn) * 9 / 10);
			
			btnNext.Location = new Point( (ClientSize.Width * 3 / 5) +dxBtn / 2,
			                              (ClientSize.Height - cyBtn) * 9 / 10);
		}
		
		void FlashCardApp_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			StringFormat sf = new StringFormat();
			
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			
			int horz = ClientSize.Width / 2;
			int vert;
			
			if (bShowCardFront == true)
			{
				vert = ClientSize.Height * 2 / 10;
				g.DrawString(cardList[nCurrentCard].Character, fChar, Brushes.Black, horz, vert, sf);
			}
			else
			{
				vert = ClientSize.Height * 2 / 10;
				g.DrawString (cardList[nCurrentCard].Pinyin, fText, Brushes.Black, horz, vert, sf);
				
				for (int n = 0; n < cardList[nCurrentCard].NumDefinitions; n++)
				{
					Definition def = cardList[nCurrentCard].getDefinition(n);
					string text = def.Grammar + ": " + def.Meaning;
					vert = ClientSize.Height * (n + 3) / 10;
					g.DrawString (text, fText, Brushes.Black, horz, vert, sf);
				}
			}
			
			string info = String.Format ("{0} {1}/{2}", numDataFiles, nCurrentCard + 1, cardList.Count);
			vert = ClientSize.Height - fInfo.Height;
			g.DrawString (info, fInfo, Brushes.Black, 0, vert);
		}
		
		void readFlashCards(string[] args)
		{
			cardList = new List<Card>();
			
			foreach (string xmlcardfile in args)
			{
				XmlDocument xmldoc = new XmlDocument();
				xmldoc.Load (xmlcardfile);
				
				XmlNodeList cardNodeList = xmldoc.GetElementsByTagName("card");
				foreach (XmlNode cardNode in cardNodeList)
				{
					Card card = new Card();
					
					XmlNodeList xcnl = cardNode.SelectNodes("character");
					card.Character = xcnl[0].InnerText;
					
					xcnl = cardNode.SelectNodes("pinyin");
					card.Pinyin = xcnl[0].InnerText;
					
					xcnl = cardNode.SelectNodes("definition");
					foreach (XmlNode xdn in xcnl)
					{
						XmlNodeList xdnl = xdn.SelectNodes("grammar");
						string grammar = xdnl[0].InnerText;
						
						xdnl = xdn.SelectNodes("meaning");
						string meaning = xdnl[0].InnerText;
						
						card.addDefinition(grammar, meaning);
					}
					
					cardList.Add(card);
				}
			}
		}
		
		void shuffleCards()
		{
			Random rng = new Random();
			
			for (int i = cardList.Count - 1; i > 0; i--)
			{
				int n = rng.Next(i+1);
				Card temp = cardList[n];
				cardList[n] = cardList[i];
				cardList[i] = temp;
			}
		}
	}
}


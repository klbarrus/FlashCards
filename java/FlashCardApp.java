import java.awt.EventQueue;
import javax.swing.JFrame;

/*
import  org.w3c.dom.bootstrap.DOMImplementationRegistry;
import  org.w3c.dom.Document;
import  org.w3c.dom.ls.DOMImplementationLS;
import  org.w3c.dom.ls.LSParser;
 */

public class FlashCardApp {

	public static void main(final String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				FlashCardFrame frame = new FlashCardFrame();
				
				frame.readFlashCards(args);
				frame.shuffleCards();				

				frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
				frame.setVisible(true);
			}
		});
	}
}
/*

try
{
	DOMImplementationRegistry registry = DOMImplementationRegistry.newInstance();
	DOMImplementationLS impl = (DOMImplementationLS)registry.getDOMImplementation("LS");
	LSParser builder = impl.createLSParser(DOMImplementationLS.MODE_SYNCHRONOUS, null);
		
	System.out.print("opening " + args[0]);
	Document document = builder.parseURI(args[0]);			
}
catch(Exception e)
{
	
}
*/

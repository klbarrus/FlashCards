import java.awt.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.*;

public class FlashCardFrame extends JFrame {
	public FlashCardFrame() {
		setTitle("Flash Card");
		setSize(DEFAULT_WIDTH, DEFAULT_HEIGHT);
		
		textPanel = new TextPanel();
		buttonPanel = new JPanel();
		prevBtn = new JButton("<");
		prevBtn.addActionListener(new prevBtnListener());
		nextBtn = new JButton(">");
		nextBtn.addActionListener(new nextBtnListener());
		buttonPanel.add(prevBtn);
		buttonPanel.add(nextBtn);
		add(buttonPanel, BorderLayout.SOUTH);
		add(textPanel, BorderLayout.CENTER);
		
		cardList = new ArrayList<Card>();
		bShowCardFront = true;
		nCurrentCard = 0;
	}
		
	private class prevBtnListener implements ActionListener {
		public void actionPerformed(ActionEvent ae) {
			if (bShowCardFront == false) {
				bShowCardFront = true;
			}
			else {
				if (nCurrentCard != 0) {
					--nCurrentCard;
					bShowCardFront = true;
				}
			}
			
			updateCard();
		}
	}
	
	private class nextBtnListener implements ActionListener {
		public void actionPerformed(ActionEvent ae) {
			if ( nCurrentCard == cardList.size() - 1 && bShowCardFront == false) {
			}
			else {
				if (bShowCardFront == true) {
					bShowCardFront = false;
				}
				else {
					++nCurrentCard;
					bShowCardFront = true;
				}
			}
			
			updateCard();
		}
	}
	
	private void updateCard() {
		textPanel.repaint();
	}
	
	private class TextPanel extends JPanel {
		public void paintComponent(Graphics g) {
			super.paintComponent(g);
			g.drawString(bShowCardFront ? "front" : "back", 50, 50);
		}
	}
	
	public void readFlashCards(final String[] args) {
		for (String file : args) {
			System.out.print("reading " + file);
		}		
		
	}
			
	public void shuffleCards() {
		
	}

	private int nCurrentCard;
	private boolean bShowCardFront;
	private ArrayList<Card> cardList;

	private JPanel buttonPanel, textPanel;
	private JButton prevBtn, nextBtn;
	private static final int DEFAULT_WIDTH = 400;
	private static final int DEFAULT_HEIGHT = 600;
}

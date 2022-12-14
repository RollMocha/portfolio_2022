package myBobPackage;

import java.awt.Color;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Rectangle;
import java.awt.RenderingHints;

import javax.swing.Icon;
import javax.swing.JButton;

public class RoundedButton extends JButton {
	private static final long serialVersionUID = 1L;

	public RoundedButton() {
		super();
		decorate();
	}

	public RoundedButton(String text) {
		super(text);
		decorate();
	}

	public RoundedButton(Icon icon) {
		super(icon);
		decorate();
	}

	public RoundedButton(String text, Icon icon) {
		super(text, icon);
		decorate();
	}

	public void decorate() {
		setBorderPainted(false);
		setContentAreaFilled(false);
		setOpaque(false);
	}

	@Override
	protected void paintComponent(Graphics g) {
		super.paintComponent(g);
		int width = getWidth();
		int height = getHeight();

		Graphics2D g2 = (Graphics2D) g;

		g2.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);

		if (getModel().isArmed()) {
			g2.setColor(getBackground().darker());
		} else if (getModel().isRollover()) {
			g2.setColor(getBackground().brighter());
		} else {
			g2.setColor(getBackground());
		}

		g2.fillRoundRect(0, 0, width, height, 20, 20);

		FontMetrics fontMetrics = g2.getFontMetrics();
		Rectangle stringBounds = fontMetrics.getStringBounds(this.getText(), g2).getBounds();

		int textX = (width - stringBounds.width) / 2;
		int textY = (height - stringBounds.height) / 2 + fontMetrics.getAscent();

		g2.setColor(Color.WHITE);
		g2.setFont(getFont());
		g2.drawString(getText(), textX, textY);
		g2.dispose();

		super.paintComponent(g);
	}

}
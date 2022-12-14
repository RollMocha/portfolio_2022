package myBobPackage;

import java.awt.Image;
import java.util.Random;

import javax.swing.ImageIcon;

public class BobEnemy {
	public static int poop_size = 60;
	Image image, gold;
	int width, height;
	int x, y;
	int SPEED;

	public BobEnemy(int x, int y) {
		this.image = new ImageIcon(new ImageIcon("src/images/poop" + (new Random()).nextInt(5) + ".png").getImage()
				.getScaledInstance(poop_size, poop_size, Image.SCALE_SMOOTH)).getImage();
		this.width = image.getWidth(null);
		this.height = image.getHeight(null);
		this.SPEED = 5 + new Random().nextInt(6) * 5;
		this.x = x;
		this.y = y;
	}

	public Image getImage() {
		return image;
	}

	public void move() {
		this.y += SPEED;
	}
}
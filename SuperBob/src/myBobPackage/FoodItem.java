package myBobPackage;

import java.awt.Image;
import java.util.*;

import javax.swing.ImageIcon;

public class FoodItem {
	
	static int food_Size = 50; 
	int x, y;
	Image img;
	int width;
	int height;
	int SPEED;
	boolean score_flag;
	
	public FoodItem(int x, int y) {
		img= new ImageIcon(new ImageIcon("src/images/food" + (new Random()).nextInt(10) + ".png").getImage().getScaledInstance(food_Size, food_Size, Image.SCALE_SMOOTH)).getImage();
		width = img.getWidth(null);
		height = img.getHeight(null);
		SPEED = 5 + new Random().nextInt(6) * 5;
		score_flag = true;
		
		this.x = x;
		this.y = y;
	}
	
	public void move() {
		this.y += SPEED;
	}
	
}

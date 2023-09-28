using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ninjagame
{


    public partial class Form1 : Form
    {



        public class actor
        {
            public int x, y,dx,shots=3;
            public Bitmap img;
            public int iframe = 0,fj=0;
            public bool dead =  false;
            
            public Rectangle src, dis;
            public List<Bitmap> imgs = new List<Bitmap>();

        }

        Bitmap off;
        int time = 0;
        int strv = 15;
        int fr = 0;
        int f1 = 1;
        int fl = 0;
        int fu, fd;
        int fjump = 0;
        int ctjump = 0;
        int jd = -1;
        int fthrow = 0;
        int fthrown = 0;
        int sbullet = 0;
        bool gravity = false;

        int health = 20;
        int frjump = 0;
        int fstairs = 0;
        int enm1 = 0;
        int enm3 = 0;
        int coinsct = 0;

        int fencect = 0;
        int siglebulletd = 0;
        Timer t = new Timer();
        List<actor> floor = new List<actor>();
        List<actor> hero = new List<actor>();
        List<actor> bullets = new List<actor>();
        List<actor> bullet = new List<actor>();
        List<actor> blocks = new List<actor>();
        List<actor> sharp = new List<actor>();
        List<actor> stairs = new List<actor>();
        List<actor> spikes = new List<actor>();
        List<actor> enemy1 = new List<actor>();
        List<actor> enemy2 = new List<actor>();
        List<actor> fence = new List<actor>();
        List<actor> lavaf = new List<actor>();
        List<actor> lavat= new List<actor>();
        List<actor> lavabt = new List<actor>();
        List<actor> laserl = new List<actor>();
        List<actor> enemy3 = new List<actor>();
        List<actor> enmbullet3= new List<actor>();
        List<actor> santa = new List<actor>();
        List<actor> ninja = new List<actor>();
        List<actor> plane = new List<actor>();
        List<actor> d = new List<actor>();



        List<actor> elvator = new List<actor>();

        List<actor> enmbullet = new List<actor>();
        List<actor> coins = new List<actor>();



        int win = 0;
        int flaser = 0;
        int xfence = 2800;
        bool walk = true;
        actor heart = new actor();
        int bgrb = 0;
        int enm2 = 0;
        int hidehero = 0;
        bool gameover = false;
        public Form1()
        {
           // this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 30);
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.WindowState = FormWindowState.Maximized;
            t.Tick += T_Tick;
            t.Interval = 100;
            t.Start();
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.L)
            {
                flaser = 0;
            }
            if (e.KeyCode == Keys.Right)
            {
                fr = 0;
                hero[0].iframe = 0;
            }
            if (e.KeyCode == Keys.Left)
            {
                fl = 0;
                hero[0].iframe = 0;

            }
            if(e.KeyCode==Keys.Up)
            {
                fu = 0;
            }

            if (e.KeyCode == Keys.Down)
            {
                fd = 0;
            }
        }

        int fj = 0;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.J)
            {
                fj = 1;
            }
            if (e.KeyCode == Keys.Up)
            {
                fu = 1;
            }
            if (e.KeyCode == Keys.Down)
            {
                fd = 1;
            }
            if (e.KeyCode == Keys.Right)
            {

                if(fr==0)
                {
                    hero[0].iframe = 10;
                }
                fr = 1;

            }
            if (e.KeyCode == Keys.Left)
            {

                if (fl == 0)
                {
                    hero[0].iframe = 40;
                }
                fl = 1;


            }
            if(e.KeyCode==Keys.Space)
            {
               
                if (ctjump < 2)
                {
                    hero[0].fj = hero[0].dis.Y - 200;
                    frjump = 0;
                }
                if(fjump==0)
                {
                    hero[0].iframe =19;
                   
                }
                fjump = 1;
                ctjump++;
                gravity = false;

            }
            if(e.KeyCode == Keys.M)
            {
                creatbullet();

               
                    hero[0].iframe = 30;
                
                fthrow = 1;

            }

            if (e.KeyCode == Keys.N)
            {
                if (sbullet == 0)
                {
                    siglebulletd = hero[0].dis.X + 1500;
                    creatsinglebullet();
                }

                if (fthrown == 0)
                {
                    hero[0].iframe = 30;
                }

                
                fthrown = 1;
                sbullet++;

            }

            if(e.KeyCode == Keys.L)
            {
                flaser = 1;
                
            }



        }
       



        int lavai = 0;
        private void T_Tick(object sender, EventArgs e)
        {
            if (win == 0 &&  !gameover)
            {

                if(health<0)
                {
                    gameover = true;
                }
                movehero();

                jumphero();
                Throw();
                movebullets();
                checkgravity();
                ishitblock();
                //checkfence();
                checkplane();
                animatesharpAndcheck();
                checkstairs();
                checkspikes();
                animateenemy1();
                animatalava();
                checklavabutton();
                touchlava();
                laser();
                elvate();
                animateenemy3();
                collectcoins();
                animatesanta();
            }
            else
            {if (!gameover)
                {
                    winnig();
                    
                }
            }
            time++;
            drawdub(this.CreateGraphics());

        }
        void winnig()
        {
            hidehero = 1;
            if (plane[0].x > 200)
            {
                plane[0].x -= 80;

            }
            else
            {
                win = 0;
                hidehero = 0;
                hero[0].dis.Y = plane[0].y;
                hero[0].dis.X = plane[0].x;
            }
            if (plane[0].x > this.Width / 2)
            {
                floor[0].src.X -= 80;
            }



        }


        
        void checkplane()
        {
            for (int i = 0; i < plane.Count; i++)
            {


                if (hero[0].dis.Y + hero[0].dis.Height > plane[i].y && hero[0].dis.Y < plane[i].y &&
                  hero[0].dis.X + hero[0].dis.Width - 20 > plane[i].x && plane[i].x + 50 > hero[0].dis.X + hero[0].dis.Width)
                {
                    win = 1;

                }
                else
                    if (hero[0].dis.Y + hero[0].dis.Height > plane[i].y && hero[0].dis.Y < plane[i].y &&
                  hero[0].dis.X - 20 < plane[i].x + 50 && plane[i].x + 50 < hero[0].dis.X + hero[0].dis.Width)
                {

                    win = 1;
                }
            }
        }
        void laser()
        {
            if(flaser ==1)
            {
                actor pnn = new actor();
                pnn.x = hero[0].dis.X + 100;
                pnn.y = hero[0].dis.Y + 70;
                Bitmap imgg = new Bitmap("laser.png");
                pnn.img = imgg;
                laserl.Add(pnn);
            }
            else
            {
                laserl.Clear();
            }
        }
        void animatalava()
        {
            lavat[lavai].iframe++;
            if (lavat[lavai].iframe > 1)
            {
                lavat[lavai].iframe = 0;
            }
            lavai++;
            if (lavai >= 7)
            {
                lavai = 0;

            }
        }
        void checkgravity()
        {
           
            if (this.Height - hero[0].dis.Height - 100 < hero[0].dis.Y )
            {
                gravity = true;
            }
            if (!gravity && fjump ==0)
            {
                hero[0].dis.Y += 20;
            }

        }
        void movehero()
        {

            if(fr==0&&fl==0 && fjump==0 && fthrow==0&&fthrown==0 && fstairs==0&& fd==0)
            {
                hero[0].iframe++;

                if (hero[0].iframe ==10 || hero[0].iframe > 10)
                {
                    hero[0].iframe=0;
                }

            }

            if (fr == 1 )
            {
                hero[0].iframe++;
                if(walk)
                {
                    if (hero[0].dis.X > this.Width / 2)
                    {


                        floor[0].src.X += 50;
                        hero[0].dis.X += 50;


                    }
                    else
                    {
                        hero[0].dis.X += 50;

                    }
                }
                
                if (hero[0].iframe == 19)
                {
                    hero[0].iframe = 10;
                }
            }
            if(fl==1)
            {
                hero[0].iframe++;

                if (hero[0].dis.X >this.Width / 2)
                {


                    floor[0].src.X -= 20;
                    hero[0].dis.X -= 20;


                }
                else
                {
                    if (hero[0].dis.X>0)
                    {
                        hero[0].dis.X -= 20;

                    }

                }
                if (hero[0].iframe == 49)
                {
                    hero[0].iframe = 40;
                }
            }
           
        }
        void jumphero()
        {
            if (fjump == 1 && gravity==false)
            {
                //if(fr==1 && frjump<2)
                //{
                //    hero[0].dis.X += 70;
                //    frjump ++;
                //}
                if (fr == 0)
                {
                    hero[0].iframe++;
                }
               //hero[0].dis.X += 20;
              
                hero[0].dis.Y += 20 *jd;
                
                if (hero[0].dis.Y <hero[0].fj || hero[0].dis.Y<10)
                {
                    jd = 1;
                }


                if (this.Height - hero[0].dis.Height-100 < hero[0].dis.Y && jd == 1 )
                {
                   
                    fjump = 0;
                    hero[0].iframe = 0;
                    jd = -1;
                    ctjump = 0;
                    hero[0].fj = 0;
                }


                if (hero[0].iframe==29)
                {
                    hero[0].iframe = 20;
                }





            }
        }

        
        void checkspikes()
        {
            for (int i = 0; i < spikes.Count; i++)
            {
               

                if (hero[0].dis.Y + hero[0].dis.Height > spikes[i].y &&
                  hero[0].dis.X + hero[0].dis.Width - 20 > spikes[i].x && spikes[i].x + 50 > hero[0].dis.X + hero[0].dis.Width)
                {
                    health--; 
                }
                else
                    if (hero[0].dis.Y + hero[0].dis.Height > spikes[i].y &&
                  hero[0].dis.X - 20 < spikes[i].x + 50 && spikes[i].x + 50 < hero[0].dis.X + hero[0].dis.Width)
                {
                    health--;
                }
            }
        }
        void ishitblock()
        {
            int f = 0;
            for (int i = 0; i < blocks.Count; i++)
            {
                //if ((hero[0].dis.Y + hero[0].dis.Height < blocks[i].y && hero[0].dis.Y + hero[0].dis.Height + 15 > blocks[i].y) &&
                //   ((hero[0].dis.X > blocks[i].x && blocks[i].x + blocks[i].img.Width > hero[0].dis.X)||
                //  (blocks[i].x >hero[0].dis.X && blocks[i].x < hero[0].dis.X + hero[0].dis.Width)))

                if (hero[0].dis.Y + hero[0].dis.Height < blocks[i].y && hero[0].dis.Y + hero[0].dis.Height +15 > blocks[i].y &&
                   ((hero[0].dis.X + hero[0].dis.Width-20 > blocks[i].x && blocks[i].x +320> hero[0].dis.X+ hero[0].dis.Width )
                ||(hero[0].dis.X < blocks[i].x && 320+ blocks[i].x < hero[0].dis.X +hero[0].dis.Width)))

                {
                    gravity = true;
                    fjump = 0;
                    jd = -1;
                    f = 1;
                    ctjump=0;


                }



            }
            if(f==0)
            { gravity = false; }

            
        }
        void Throw()
        {
            if (fthrow == 1)
            {

                if (hero[0].iframe == 39)
                {
                    fthrow = 0;
                    hero[0].iframe = 0;
                    if (fjump == 1)
                    {
                        hero[0].iframe = 20;
                    }

                }
                hero[0].iframe++;


            }
            else
            {
                if (fthrown == 1)
                {

                    if (hero[0].iframe == 39)
                    {
                        fthrown = 0;
                        hero[0].iframe = 0;
                        if (fjump == 1)
                        {
                            hero[0].iframe = 20;
                        }
                    }
                    hero[0].iframe++;


                }
            }


        }

        void movebullets()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].x += 50;
                if (bullets[i].x > bullets[i].fj)
                {
                    bullets.RemoveAt(i);
                }
            }
            for (int i = 0; i < bullet.Count; i++)
            {
                bullet[i].x += 50;
                if (bullet[i].x > bullet[i].fj)
                {
                    bullet.RemoveAt(i);
                    sbullet = 0;
                }
            }

        }
        void creatbullet()
        {
            actor pnn = new actor();
           Bitmap img = new Bitmap("hero//Kunai.png");
            pnn.x = hero[0].dis.X + hero[0].dis.Width + 5;
            pnn.fj = pnn.x+ 1000;
            pnn.img = img;
            pnn.y = hero[0].dis.Y + 100;
            bullets.Add(pnn);
        }
        void creathero()
        {
            actor pnn = new actor();
            Bitmap img;
            pnn = new actor();
            img = new Bitmap("hero\\Idle__00" + 0 + ".png");

            pnn.dis = new Rectangle(0,450, 120,180);
            pnn.src = new Rectangle(0, 0,img.Width, img.Height);

            for (int i = 0; i < 10; i++)
            {


                img = new Bitmap("hero\\Idle__00" + i + ".png");
                pnn.src = new Rectangle(0, 0, img.Width, img.Height);

                pnn.img = img;
                pnn.imgs.Add(img);



            }

            for (int i = 0; i < 10; i++)
            {


                img = new Bitmap("hero\\Run__00" + i + ".png");
                pnn.src = new Rectangle(0, 0, img.Width, img.Height);

                pnn.img = img;
                pnn.imgs.Add(img);



            }
            for (int i = 0; i < 10; i++)
            {


                img = new Bitmap("hero\\Jump__00" + i + ".png");
                pnn.src = new Rectangle(0, 0, img.Width, img.Height);

                pnn.img = img;
                pnn.imgs.Add(img);



            }
            for (int i = 0; i < 10; i++)
            {


                img = new Bitmap("hero\\Throw__00" + i + ".png");
                pnn.src = new Rectangle(0, 0, img.Width, img.Height);

                pnn.img = img;
                pnn.imgs.Add(img);



            }

            for (int i = 0; i < 10; i++)
            {


                img = new Bitmap("hero\\RunR__00" + i + ".png");
                pnn.src = new Rectangle(0, 0, img.Width, img.Height);

                pnn.img = img;
                pnn.imgs.Add(img);



            }
            for (int i = 0; i < 10; i++)
            {


                img = new Bitmap("hero\\Climb_00" + i + ".png");
                pnn.src = new Rectangle(0, 0, img.Width, img.Height);

                pnn.img = img;
                pnn.imgs.Add(img);



            }




            hero.Add(pnn);

        }
        void creatsinglebullet()
        {
            actor pnn = new actor();
            Bitmap img = new Bitmap("hero\\Kunai.png");
            pnn.x = hero[0].dis.X + hero[0].dis.Width + 5;
            pnn.fj = pnn.x + 600;
            pnn.img = img;
            pnn.y = hero[0].dis.Y + 100;
            bullet.Add(pnn);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawdub(e.Graphics);
        }
        void createblocks()
        {
            actor pnn = new actor();
            Bitmap img  = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 500 ;
            pnn.y = 500;
            creatcoin(550, 400);
            blocks.Add(pnn);


            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 1250;
            pnn.y = 300;
            creatcoin(1300, 200);
            creatcoin(1500, 200);


            blocks.Add(pnn);

            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 1250+img.Width;
            pnn.y = 300;
            creatcoin(pnn.x-150, pnn.y-100);

            blocks.Add(pnn);

            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 2600;
            pnn.y = 500;
            creatcoin(pnn.x - 150, pnn.y - 100);
            creatcoin(pnn.x - 150+200, pnn.y - 100);


            blocks.Add(pnn);

            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 2300+1040;
            pnn.y = 500;
            creatcoin(pnn.x - 150, pnn.y - 100);

            blocks.Add(pnn);

            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 4700;
            pnn.y = 200;
            creatcoin(pnn.x - 150, pnn.y - 100);

            blocks.Add(pnn);

            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 4700+ img.Width;
            pnn.y = 200;
            creatcoin(pnn.x - 150, pnn.y - 100);

            blocks.Add(pnn);

            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 4700 + img.Width+ img.Width;
            pnn.y = 200;
            creatcoin(pnn.x - 150, pnn.y - 100);

            blocks.Add(pnn);

            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 4700 + img.Width + img.Width+ img.Width;
            pnn.y = 200;
            creatcoin(pnn.x - 150, pnn.y - 100);

            blocks.Add(pnn);



            pnn = new actor();
            img = new Bitmap("elementStone013.png");
            pnn.img = img;
            pnn.x = 4700 + img.Width + img.Width + img.Width+ img.Width;
            pnn.y = 200;
            creatcoin(pnn.x - 150, pnn.y - 100);

            blocks.Add(pnn);

          
            



        }
        void  animatesharpAndcheck()
        {
            for (int i = 0; i < sharp.Count; i++)
            {
                sharp[i].iframe++;
                if (sharp[i].iframe > 1)
                {
                    sharp[i].iframe = 0;
                }
                sharp[i].x += 15 * sharp[i].dx;

                if (time % 30 == 0)
                {
                    sharp[i].dx *= -1;
                }


                if (hero[0].dis.Y + hero[0].dis.Height > sharp[i].y &&
                  hero[0].dis.X + hero[0].dis.Width -20 > sharp[i].x && sharp[i].x +50> hero[0].dis.X + hero[0].dis.Width)
                {
                    scroll('l', 20);
                    health--;
                   hero[0].dis.X -= 20;
                }
                else
                    if(hero[0].dis.Y + hero[0].dis.Height > sharp[i].y &&
                  hero[0].dis.X - 20 < sharp[i].x +50 && sharp[i].x+50 < hero[0].dis.X + hero[0].dis.Width )
                {
                    scroll('r', 20);
                    health--;
                    hero[0].dis.X += 20;
                }
            }
            


        }
        void createlvator()
        {
            actor pnn = new actor();
            pnn.x = 4500;
            pnn.y = 580;
            Bitmap img = new Bitmap("elvator.png");
            pnn. img = img;
            elvator.Add(pnn);
        }

        void createlavabt()
        {
           actor pnn = new actor();
            pnn.x = 2760;
            pnn.y = 450;
            Bitmap img = new Bitmap("switchRed.png");
            pnn.imgs.Add(img);
            img = new Bitmap("switchRed_pressed.png");
            pnn.imgs.Add(img);
            lavabt.Add(pnn);
        }

        void checkfence()
        {
            if (fencect > 1)
            {

                int f = 0;
                for (int i = 0; i < fence.Count; i++)
                {
                    //if ((hero[0].dis.Y + hero[0].dis.Height < blocks[i].y && hero[0].dis.Y + hero[0].dis.Height + 15 > blocks[i].y) &&
                    //   ((hero[0].dis.X > blocks[i].x && blocks[i].x + blocks[i].img.Width > hero[0].dis.X)||
                    //  (blocks[i].x >hero[0].dis.X && blocks[i].x < hero[0].dis.X + hero[0].dis.Width)))

                    if (hero[0].dis.Y + hero[0].dis.Height < fence[i].y && hero[0].dis.Y + hero[0].dis.Height + 15 > fence[i].y &&
                       ((hero[0].dis.X + hero[0].dis.Width - 20 > fence[i].x && fence[i].x + 320 > hero[0].dis.X + hero[0].dis.Width)
                    || (hero[0].dis.X < fence[i].x && 320 + fence[i].x < hero[0].dis.X + hero[0].dis.Width)))

                    {
                        gravity = true;
                        fjump = 0;
                        jd = -1;
                        f = 1;
                        ctjump = 0;


                    }



                }
                if (f == 0)
                { gravity = false; }
            }
        }
        void checklavabutton()
        {
            if (hero[0].dis.Y + hero[0].dis.Height > 450 && hero[0].dis.Y + hero[0].dis.Height - 100 < 450 &&
                hero[0].dis.X > 2700 && hero[0].dis.X < 2800)

            {
                lavabt[0].iframe = 1;
                if (time % 10 == 0)
                {
                    if (fencect < 5)
                    {
                        creatfence();

                    }
                    fencect++;
                }
            }
            else
            {
                lavabt[0].iframe = 0;

            }
            int w = 0;

            for (int i = 0; i < lavaf.Count; i++)
            {
                if (hero[0].dis.X + hero[0].dis.Width + 50> lavaf[i].x &&
                   hero[0].dis.X + hero[0].dis.Width < lavaf[i].x &&
                   hero[0].dis.Y > 500 &&
                    hero[0].dis.Y < this.Height)
                {
                    w = 1;
                }
               
            }
            if(w==1)
            {
                walk = false;
            }
            else
            {
                walk = true;
            }
        }
        void creatlave()
        {

            actor pnn = new actor();
            pnn.x = 2600;
            pnn.y = 550;
            Bitmap img = new Bitmap("bridgeB.png");
            pnn.img = img;
            lavaf.Add(pnn);

             pnn = new actor();
            pnn.x = 2600;
            pnn.y = 650;
            img = new Bitmap("bridgeB.png");
            pnn.img = img;
            lavaf.Add(pnn);

            pnn = new actor();
            pnn.x = 2600+930;
            pnn.y = 550;
             img = new Bitmap("bridgeB.png");
            pnn.img = img;
            lavaf.Add(pnn);

            pnn = new actor();
            pnn.x = 2600 + 930;
            pnn.y = 650;
            img = new Bitmap("bridgeB.png");
            pnn.img = img;
            lavaf.Add(pnn);

            int x = 2640;

            for (int i = 0; i < 7; i++)
            {
                pnn = new actor();
                pnn.x = x;
                pnn.y = 650;
                img = new Bitmap("lavaTop_high.png");
                x += img.Width;
                pnn.imgs.Add(img);
                img = new Bitmap("lavaTop_low.png");
                pnn.imgs.Add(img);
                lavat.Add(pnn);
            }
           

        }
        void touchlava()
        {
            for (int i = 0; i < lavat.Count; i++)
            {


                if (hero[0].dis.Y + hero[0].dis.Height > lavat[i].y &&
                  hero[0].dis.X + hero[0].dis.Width - 20 > lavat[i].x && lavat[i].x + 50 > hero[0].dis.X + hero[0].dis.Width)
                {
                    health--;
                }
                else
                    if (hero[0].dis.Y + hero[0].dis.Height > lavat[i].y &&
                  hero[0].dis.X - 20 < lavat[i].x + 50 && lavat[i].x + 50 < hero[0].dis.X + hero[0].dis.Width)
                {
                    health--;
                }
            }
        }
        void creatfence()
        {
            actor pnn = new actor();
            pnn.x = xfence;
            pnn.y = 500;
            Bitmap img = new Bitmap("bridgeA.png");
            pnn.img = img;
            xfence += img.Width;
            blocks.Add(pnn);
        }
        bool scroll(char d,int m)
        {
            if (hero[0].dis.X > this.Width / 2)
            {
                if(d=='l')
                {
                    floor[0].src.X -= m; 
                }
                else
                {
                    floor[0].src.X += m;

                }
                return true;
            }

            return false;
        }
        void creatsharp()
        {
            actor pnn = new actor();
           
            pnn.x = 800;
            pnn.y = this.Height-150;
           
            Bitmap img = new Bitmap("platformIndustrial_068.png");
            pnn.img = img;
            pnn.dx = 1;
            pnn.imgs.Add(img);
             img = new Bitmap("platformIndustrial_069.png");
            pnn.img = img;
            pnn.imgs.Add(img);
            sharp.Add(pnn);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.Width, this.Height);

            actor pnn = new actor();
            pnn.dis = new Rectangle(0, 0, this.Width, this.Height);
            Bitmap img = new Bitmap("BK2.png");
            pnn.src = new Rectangle(0, 800, this.Width, this.Height);
            pnn.img = img;
            floor.Add(pnn);


            creathero();
            createblocks();
            creatsharp();
            creathealth();
            creatstairs();
            creatspikes();
            creatlave();
            createnemy1();
            createlavabt();
            createenemy2();
            createlvator();

            createnemy3();

            creatcoinsmap();
            creatsanta();

        }
        void createnemy3()
        {
            actor pnn = new actor();
            pnn.x = 4800;
            pnn.y = 100;
            pnn.dx = -1;
            for (int i = 0; i < 9; i++)
            {
                Bitmap img = new Bitmap("enemy 3\\__alien_enemy_4_purple_green_flying_00" + i + ".png");
                pnn.imgs.Add(img);
            }
            for (int i = 0; i < 9; i++)
            {
                Bitmap img = new Bitmap("enemy 3\\__alien_enemy_4_purple_green_flying_bomb_hatch_open_00" + i + ".png");
                pnn.imgs.Add(img);

            }


            enemy3.Add(pnn);
        }
        void animatesanta()
        {
            santa[0].iframe++;
            if (santa[0].iframe>=16)
            {
                santa[0].iframe=0;

            }
        }
        void animateenemy3()
        {
            if (enm3 == 0)
            {
                enemy3[0].x += 20* enemy3[0].dx;
                if (enemy3[0].dead == false)
                {
                    enemy3[0].iframe++;

                }
                if (enemy3[0].iframe >= 18 && enemy3[0].dead == false)
                {
                    enemy3[0].iframe = 0;

                }
                if (time % 20 == 0 && enemy3[0].dead == false)
                {
                    enemy3[0].dx *= -1;
                }

                if (time % 10== 0 && enemy3[0].dead == false)
                {
                    createenmbullet3();
                }




                if (hero[0].dis.Y + hero[0].dis.Height > enemy3[0].y &&
                  hero[0].dis.X + hero[0].dis.Width - 20 > enemy3[0].x && enemy3[0].x + 50 > hero[0].dis.X + hero[0].dis.Width)
                {
                    health--;
                }
                else
                    if (hero[0].dis.Y + hero[0].dis.Height > enemy3[0].y &&
                  hero[0].dis.X - 20 < enemy3[0].x + 50 && enemy3[0].x + 50 < hero[0].dis.X + hero[0].dis.Width)
                {
                    health--;
                }

                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].x + bullets[i].img.Width > enemy3[0].x && enemy3[0].x + 200 > bullets[i].x + bullets[i].img.Width &&
                        bullets[i].y > enemy3[0].y && bullets[i].y < enemy3[0].y + 300)
                    {
                        enemy3[0].shots--;
                        bullets.RemoveAt(i);
                    }
                }
                for (int i = 0; i < bullet.Count; i++)
                {
                    if (bullet[i].x + bullet[i].img.Width > enemy3[0].x && enemy3[0].x + 200 > bullet[i].x + bullet[i].img.Width &&
                        bullet[i].y > enemy3[0].y && bullet[i].y < enemy3[0].y + 300)
                    {
                        enemy3[0].shots--;
                        bullet.RemoveAt(i);
                        sbullet = 0;

                    }
                }

                for (int i = 0; i < laserl.Count; i++)
                {
                    if (laserl[i].x + laserl[i].img.Width > enemy3[0].x && enemy3[0].x + 200 > laserl[i].x + laserl[i].img.Width &&
                        laserl[i].y > enemy3[0].y && laserl[i].y < enemy3[0].y + 300)
                    {
                        enemy3[0].shots--;
                        flaser = 0;
                        sbullet = 0;

                    }
                    else
                    {
                        if (laserl[i].x < enemy3[0].x && enemy3[0].x + 200 < laserl[i].x + laserl[i].img.Width &&
                       laserl[i].y > enemy3[0].y && laserl[i].y < enemy3[0].y + 300)
                        {
                            enemy3[0].shots--;
                            flaser = 0;
                            sbullet = 0;
                        }
                    }
                }

                if (enemy3[0].shots <= 0 && enemy3[0].dead == false)
                {
                    enemy3[0].dead = true;

                }

                if (enemy3[0].dead)
                {
                    creatcoin(enemy3[0].x, enemy3[0].y);

                    enemy3.Clear();
                    enm3 = 1;

                }
            }
            for (int i = 0; i < enmbullet3.Count; i++)
            {
                enmbullet3[i].y += 50;
                if (enmbullet3[i].x < hero[0].dis.X + hero[0].dis.Width &&
                    enmbullet3[i].x > hero[0].dis.X &&
                    enmbullet3[i].y > hero[0].dis.Y && enmbullet3[i].y < hero[0].dis.Y + hero[0].dis.Width)
                {
                    enmbullet3.RemoveAt(i);
                    health--;
                }
                else
                {
                    if (enmbullet3[i].y>800)
                    {
                        enmbullet3.RemoveAt(i);

                    }
                }
            }
        }
        void elvate()
        {
            int f = 0;
            if (hero[0].dis.X > elvator[0].x && hero[0].dis.X + hero[0].dis.Width < elvator[0].x + elvator[0].img.Width
                && hero[0].dis.Y+50 >elvator[0].y && hero[0].dis.Y+ hero[0].dis.Height < elvator[0].y + elvator[0].img.Height)
            {
                f = 1;

            }
            if(f==1 )
            {
                if (elvator[0].y > 30)
                {
                    hero[0].dis.Y -= 20;
                    elvator[0].y -= 20;
                }
                gravity = true;
            }
            if(f == 0)
            {
                if (elvator[0].y < 580)
                {
                    elvator[0].y += 20;
                }
            }
        }
        void createenemy2()
        {
            actor pnn = new actor();
            pnn.x = 4400;
            pnn.y = 550;
            for (int i = 0; i < 9; i++)
            {
                Bitmap img = new Bitmap("enemy 2\\Attack_2_00" + i + ".png");
                pnn.imgs.Add(img);
            }
            enemy2.Add(pnn);
        }
        void animateenemy1()
        {
            if (enm1 == 0)
            {


                enemy1[0].iframe++;
                enemy1[0].x += enemy1[0].dx * 15;
                if (enemy1[0].dx == 1 && enemy1[0].iframe > 5 && enemy1[0].dead == false)
                {
                    enemy1[0].iframe = 0;
                }

                if (enemy1[0].dx == -1 && enemy1[0].iframe > 10 && enemy1[0].dead == false)
                {
                    enemy1[0].iframe = 6;
                }

                if (time % 30 == 0)
                {
                    enemy1[0].dx *= -1;
                }

                


                    if (hero[0].dis.Y + hero[0].dis.Height > enemy1[0].y &&
                      hero[0].dis.X + hero[0].dis.Width - 20 > enemy1[0].x && enemy1[0].x + 50 > hero[0].dis.X + hero[0].dis.Width)
                    {
                        health--;
                    }
                    else
                        if (hero[0].dis.Y + hero[0].dis.Height > enemy1[0].y &&
                      hero[0].dis.X - 20 < enemy1[0].x + 50 && enemy1[0].x + 50 < hero[0].dis.X + hero[0].dis.Width)
                    {
                        health--;
                    }
                
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].x + bullets[i].img.Width > enemy1[0].x && enemy1[0].x + 200 > bullets[i].x + bullets[i].img.Width &&
                        bullets[i].y > enemy1[0].y && bullets[i].y < enemy1[0].y + 300)
                    {
                        enemy1[0].shots--;
                        bullets.RemoveAt(i);
                    }
                }
                for (int i = 0; i < bullet.Count; i++)
                {
                    if (bullet[i].x + bullet[i].img.Width > enemy1[0].x && enemy1[0].x + 200 > bullet[i].x + bullet[i].img.Width &&
                        bullet[i].y > enemy1[0].y && bullet[i].y < enemy1[0].y + 300)
                    {
                        enemy1[0].shots--;
                        bullet.RemoveAt(i);
                        sbullet = 0;

                    }
                }

                for (int i = 0; i < laserl.Count; i++)
                {
                    if (laserl[i].x + laserl[i].img.Width > enemy1[0].x && enemy1[0].x + 200 > laserl[i].x + laserl[i].img.Width &&
                        laserl[i].y > enemy1[0].y && laserl[i].y < enemy1[0].y + 300)
                    {
                        enemy1[0].shots--;
                        flaser = 0;
                        sbullet = 0;

                    }
                    else
                    {
                        if (laserl[i].x  < enemy1[0].x && enemy1[0].x + 200 <laserl[i].x + laserl[i].img.Width &&
                       laserl[i].y > enemy1[0].y && laserl[i].y < enemy1[0].y + 300)
                        {
                            enemy1[0].shots--;
                            flaser = 0;
                            sbullet = 0;
                        }
                    }
                }

                if (enemy1[0].shots <= 0 && enemy1[0].dead == false)
                {
                    enemy1[0].dead = true;
                    if (enemy1[0].dx == 1)
                    {
                        enemy1[0].iframe = 10;
                    }
                    else
                    {
                        enemy1[0].iframe = 14;
                    }
                }

                if (enemy1[0].dead)
                {
                    if (enemy1[0].dx == 1)
                    {
                        if (enemy1[0].iframe >= 15)
                        {
                            creatcoin(enemy1[0].x, enemy1[0].y);
                            enemy1.Clear();
                            enm1 = 1;
                        }
                    }
                    else
                    {
                        if (enemy1[0].iframe >= 18)
                        {
                            creatcoin(enemy1[0].x, enemy1[0].y);
                            enemy1.Clear();
                            enm1 = 1;
                        }
                    }
                }
            }

            if(enm2==0)
            {
                if (enemy2[0].dead==false)
                {
                    enemy2[0].iframe++;

                }
                if (enemy2[0].iframe>=8 && enemy2[0].dead==false)
                {
                    enemy2[0].iframe=0;

                }
                if (time % 20==0&&enemy2[0].dead == false)
                {
                    createenmbullet();
                }




                if (hero[0].dis.Y + hero[0].dis.Height > enemy2[0].y &&
                  hero[0].dis.X + hero[0].dis.Width - 20 > enemy2[0].x && enemy2[0].x + 50 > hero[0].dis.X + hero[0].dis.Width)
                {
                    health--;
                }
                else
                    if (hero[0].dis.Y + hero[0].dis.Height > enemy2[0].y &&
                  hero[0].dis.X - 20 < enemy2[0].x + 50 && enemy2[0].x + 50 < hero[0].dis.X + hero[0].dis.Width)
                {
                    health--;
                }

                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].x + bullets[i].img.Width > enemy2[0].x && enemy2[0].x + 200 > bullets[i].x + bullets[i].img.Width &&
                        bullets[i].y > enemy2[0].y && bullets[i].y < enemy2[0].y + 300)
                    {
                        enemy2[0].shots--;
                        bullets.RemoveAt(i);
                    }
                }
                for (int i = 0; i < bullet.Count; i++)
                {
                    if (bullet[i].x + bullet[i].img.Width > enemy2[0].x && enemy2[0].x + 200 > bullet[i].x + bullet[i].img.Width &&
                        bullet[i].y > enemy2[0].y && bullet[i].y < enemy2[0].y + 300)
                    {
                        enemy2[0].shots--;
                        bullet.RemoveAt(i);
                        sbullet = 0;

                    }
                }

                for (int i = 0; i < laserl.Count; i++)
                {
                    if (laserl[i].x + laserl[i].img.Width > enemy2[0].x && enemy2[0].x + 200 > laserl[i].x + laserl[i].img.Width &&
                        laserl[i].y > enemy2[0].y && laserl[i].y < enemy2[0].y + 300)
                    {
                        enemy2[0].shots--;
                        flaser = 0;
                        sbullet = 0;

                    }
                    else
                    {
                        if (laserl[i].x < enemy2[0].x && enemy2[0].x + 200 < laserl[i].x + laserl[i].img.Width &&
                       laserl[i].y > enemy2[0].y && laserl[i].y < enemy2[0].y + 300)
                        {
                            enemy2[0].shots--;
                            flaser = 0;
                            sbullet = 0;
                        }
                    }
                }

                if (enemy2[0].shots <= 0 && enemy2[0].dead == false)
                {
                    enemy2[0].dead = true;
                   
                }

                if (enemy2[0].dead)
                {
                    creatcoin(enemy2[0].x, enemy2[0].y);

                    enemy2.Clear();
                            enm2 = 1;
                   
                }
            }
            for (int i = 0; i < enmbullet.Count; i++)
            {
                enmbullet[i].x -= 50;
                if (enmbullet[i].x < hero[0].dis.X + hero[0].dis.Width &&
                    enmbullet[i].x > hero[0].dis.X &&
                    enmbullet[i].y > hero[0].dis.Y && enmbullet[i].y < hero[0].dis.Y + hero[0].dis.Width)
                {
                    enmbullet.RemoveAt(i);
                    health--;
                }
                else
                {
                    if (enmbullet[i].x < 3500)
                    {
                        enmbullet.RemoveAt(i);

                    }
                }
            }

        }
        void createenmbullet()
        {
            if(enm2==0)
            {
                actor pnn = new actor();
                pnn.x = enemy2[0].x - 50;
                pnn.y = enemy2[0].y +120;
                Bitmap img = new Bitmap("enemy 2\\rocket1.png");
                pnn.img = img;
                enmbullet.Add(pnn);


            }
        }
        void createenmbullet3()
        {
            actor pnn = new actor();
            pnn.x = enemy3[0].x + 50;
            pnn.y = enemy3[0].y + 120;
            Bitmap img = new Bitmap("enemy 3\\fireball.png");
            pnn.img = img;
            enmbullet3.Add(pnn);
        }
        void creatcoin(int x,int y)
        {
            actor pnn = new actor();
            pnn.x = x;
            pnn.y = y+50;
            Bitmap img = new Bitmap("hud_coins.png");
            pnn.img = img;

            coins.Add(pnn);
        }

        void creatsanta()
        {
            actor pnn = new actor();
            pnn.x = 200;
            pnn.y =520 ;
            for (int i = 1; i < 17; i++)
            {
                Bitmap img = new Bitmap("santa\\Idle (" + i + ").png");
                pnn.imgs.Add(img);
            }
            santa.Add(pnn);
        }
        void creatcoinsmap()
        {
            actor pnn = new actor();
            pnn.x = 580;
            pnn.y = 450;
            Bitmap img = new Bitmap("hud_coins.png");
            pnn.img = img;

            coins.Add(pnn);


            pnn = new actor();
            Bitmap imgll = new Bitmap("gemRed.png");
            pnn.img = imgll;
            pnn.x = 5600;
            pnn.y = 30;
            coins.Add(pnn);


            pnn = new actor();
             imgll = new Bitmap("planeRed1.png");
            pnn.img = imgll;
            pnn.x = 5800;
            pnn.y = 30;
            plane.Add(pnn);

        }

        void collectcoins()
        {
            for (int i = 0; i < coins.Count; i++)
            {


                if (hero[0].dis.Y + hero[0].dis.Height > coins[i].y && hero[0].dis.Y < coins[i].y&&
                  hero[0].dis.X + hero[0].dis.Width - 20 > coins[i].x && coins[i].x + 50 > hero[0].dis.X + hero[0].dis.Width)
                {
                    coinsct++;
                    coins.RemoveAt(i);
                }
                else
                    if (hero[0].dis.Y + hero[0].dis.Height > coins[i].y && hero[0].dis.Y < coins[i].y&&
                  hero[0].dis.X - 20 < coins[i].x + 50 && coins[i].x + 50 < hero[0].dis.X + hero[0].dis.Width)
                {
                    coinsct++;
                    coins.RemoveAt(i);

                }
            }
        }

        void createnemy1()
        {
            actor pnn = new actor();
            pnn.x = 2000;
           pnn.y = 550;
            pnn.dx = -1;
           pnn.iframe = 6;
            for (int i = 0; i < 6; i++)
            {
                Bitmap img = new Bitmap("enemy1\\run_00"+i+".png");
                pnn.imgs.Add(img);
            }
            for (int i = 0; i < 6; i++)
            {
                Bitmap img = new Bitmap("enemy1\\run2_00" + i + ".png");
                pnn.imgs.Add(img);
            }
            for (int i = 0; i < 6; i++)
            {
                Bitmap img = new Bitmap("enemy1\\Death_00"+i+".png");
                pnn.imgs.Add(img);
            }
            for (int i = 0; i < 6; i++)
            {
                Bitmap img = new Bitmap("enemy1\\Death2_00" + i + ".png");
                pnn.imgs.Add(img);
            }


            enemy1.Add(pnn);


        }

        void creatspikes()
        {
            int x = 1400;
            for (int i = 0; i < 5; i++)
            {
                actor pnn = new actor();

                Bitmap img = new Bitmap("spikes.png");

                pnn.x = x;
                pnn.y = this.Height - 180;

                pnn.img = img;
                x = x+img.Width;
                spikes.Add(pnn);
            }
            
        }

        void checkstairs()
        {
            int f = 0;

            for (int i = 0; i < stairs.Count; i++)
            {
                if (hero[0].dis.X > stairs[i].x-40 && hero[0].dis.X + hero[0].dis.Width < stairs[i].x + stairs[i].img.Width+50)
                {
                    gravity = true;
                    if (fu == 1)
                    {
                        
                        if (hero[0].dis.Y + hero[0].dis.Height> stairs[i].y)
                        {
                            if (fstairs == 0)
                            {
                                hero[0].iframe = 50;
                            }
                            fstairs =1; 
                         
                            hero[0].dis.Y -= 20;
                        }
                        else
                        {
                            fstairs = 0;                 
                        }
                        if(fstairs==1 && fu==1)
                        {
                            hero[0].iframe++;

                            if (hero[0].iframe > 59)
                            {
                                hero[0].iframe = 50;
                            }
                        }
                    }
                    if(fd==1)
                    {

                        if (hero[0].dis.Y < 580)
                        {
                            if (fstairs == 0)
                            {
                                hero[0].iframe = 50;
                            }
                            fstairs = 1;

                            hero[0].dis.Y += 20;
                        }
                        else
                        {
                            fstairs = 0;
                        }
                        if (fstairs == 1 && fd==1)
                        {
                            hero[0].iframe--;

                            if (hero[0].iframe <50)
                            {
                                hero[0].iframe = 59;
                            }
                        }
                    }
                }
                else
                {
                    fstairs = 0;
                }

            }
            
        }
        void creatstairs()
        {
            actor pnn = new actor();

            pnn.x = 1250;
            pnn.y = 300;

            Bitmap img = new Bitmap("stairs1.png");
            pnn.img = img;
            pnn.dx = 1;
            stairs.Add(pnn);
        }
        void creathealth()
        {
           
            heart.x = 20;
            heart.y = 10;
            Bitmap img = new Bitmap("gemRed.png");
            heart.img = img;
        }
        void drawdub(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawscene(g2);
            g.DrawImage(off, 0, 0);
        }
        void drawscene(Graphics g2)
        {
           
            g2.DrawImage(floor[0].img, floor[0].dis, floor[0].src, GraphicsUnit.Pixel);

            for (int i = 0; i < bullets.Count; i++)
            {
                g2.DrawImage(bullets[i].img, bullets[i].x - floor[0].src.X, bullets[i].y- floor[0].src.Y + 800);
            }

            for (int i = 0; i < bullet.Count; i++)
            {
                g2.DrawImage(bullet[i].img, bullet[i].x - floor[0].src.X , bullet[i].y - floor[0].src.Y + 800);
            }
            for (int i = 0; i < sharp.Count; i++)
            {
                int h = sharp[i].iframe;
                g2.DrawImage(sharp[i].imgs[h], sharp[i].x - floor[0].src.X, sharp[i].y - floor[0].src.Y + 800);
            }

            for (int i = 0; i < stairs.Count; i++)
            {
                g2.DrawImage(stairs[i].img, stairs[i].x - floor[0].src.X, stairs[i].y - floor[0].src.Y+800);
            }
            for (int i = 0; i < fence.Count; i++)
            {
                g2.DrawImage(fence[i].img, fence[i].x - floor[0].src.X, fence[i].y - floor[0].src.Y + 800);

            }

            for (int i = 0; i < blocks.Count; i++)
            {
                SolidBrush b = new SolidBrush(Color.White);

                g2.DrawImage(blocks[i].img, blocks[i].x - floor[0].src.X, blocks[i].y - floor[0].src.Y + 800);


            }
            for (int i = 0; i < spikes.Count; i++)
            {
                g2.DrawImage(spikes[i].img, spikes[i].x - floor[0].src.X, spikes[i].y - floor[0].src.Y + 800);
            }

            for (int i = 0; i < plane.Count; i++)
            {
                g2.DrawImage(plane[i].img, plane[i].x - floor[0].src.X, plane[i].y - floor[0].src.Y + 800);
            }
            if (enm1==0)
            {
                int jj = enemy1[0].iframe;
                g2.DrawImage(enemy1[0].imgs[jj], enemy1[0].x - floor[0].src.X, enemy1[0].y - floor[0].src.Y + 800);

            }

            int ll = santa[0].iframe;
            g2.DrawImage(santa[0].imgs[ll], santa[0].x - floor[0].src.X, santa[0].y - floor[0].src.Y + 800);
            //Bitmap imgk = new Bitmap()

            if (enm2 == 0)
            {
                int jj = enemy2[0].iframe;
                g2.DrawImage(enemy2[0].imgs[jj], enemy2[0].x - floor[0].src.X, enemy2[0].y - floor[0].src.Y + 800);

            }
            if (enm3 == 0)
            {
                int jj = enemy3[0].iframe;
                g2.DrawImage(enemy3[0].imgs[jj], enemy3[0].x - floor[0].src.X, enemy3[0].y - floor[0].src.Y + 800);

            }

            for (int i = 0; i < lavat.Count; i++)
            {
                int gg = lavat[i].iframe;
                g2.DrawImage(lavat[i].imgs[gg], lavat[i].x - floor[0].src.X, lavat[i].y - floor[0].src.Y + 800);

            }
            for (int i = 0; i < lavaf.Count; i++)
            {
                g2.DrawImage(lavaf[i].img, lavaf[i].x - floor[0].src.X, lavaf[i].y - floor[0].src.Y + 800);

            }

            for (int i = 0; i < laserl.Count; i++)
            {
                g2.DrawImage(laserl[i].img, laserl[i].x - floor[0].src.X, laserl[i].y - floor[0].src.Y + 800);

            }
            for (int i = 0; i < elvator.Count; i++)
            {
                g2.DrawImage(elvator[i].img, elvator[i].x - floor[0].src.X, elvator[i].y - floor[0].src.Y + 800);

            }

            // g2.DrawImage(heart.img, heart.x, heart.y);
            int mm = lavabt[0].iframe;
            g2.DrawImage(lavabt[0].imgs[mm], lavabt[0].x - floor[0].src.X, lavabt[0].y - floor[0].src.Y + 800);
            int k = hero[0].iframe;
            if (hero[0].dis.X > this.Width / 2)
            {
                hero[0].dis.X -= floor[0].src.X;
            }
            for (int i = 0; i < enmbullet.Count; i++)
            {
                g2.DrawImage(enmbullet[i].img, enmbullet[i].x - floor[0].src.X, enmbullet[i].y - floor[0].src.Y + 800);

            }

            for (int i = 0; i < enmbullet3.Count; i++)
            {
                g2.DrawImage(enmbullet3[i].img, enmbullet3[i].x - floor[0].src.X, enmbullet3[i].y - floor[0].src.Y + 800);

            }
            int x = 20;
            for (int i = 0; i < health; i++)
            {
                Bitmap img = new Bitmap("hud_heartFull.png");
                g2.DrawImage(img, x, 20);
                x += img.Width;

            }
            Bitmap imghh = new Bitmap("sign1.png");
            g2.DrawImage(imghh, 450 -floor[0].src.X, 600);

            Bitmap imgpp = new Bitmap("sign2.png");
            g2.DrawImage(imgpp, 5500 - floor[0].src.X, 120);

            for (int i = 0; i < coins.Count; i++)
            {
                g2.DrawImage(coins[i].img, coins[i].x - floor[0].src.X, coins[i].y - floor[0].src.Y + 800);

            }
                if (hidehero == 0)
                {
                    g2.DrawImage(hero[0].imgs[k], hero[0].dis, hero[0].src, GraphicsUnit.Pixel);
                    if (hero[0].dis.X > this.Width / 2)
                    {
                        hero[0].dis.X += floor[0].src.X;

                    }
                }
            imgpp = new Bitmap("coinGold.png");
            g2.DrawImage(imgpp, 20, 40);
            g2.DrawString(coinsct + "", new Font("console", 20), Brushes.White, + 70, 50);



            if (gameover)
            {
                 imgpp = new Bitmap("textGameOver.png");
                g2.DrawImage(imgpp, this.Width/2-200 ,300 );

            }

        }

    }
}


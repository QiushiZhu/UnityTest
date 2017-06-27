## Hunting Souls开发日志 ##


### 4.25 ###
尝试了两种低层级对象通知高层级对象的实现方式：


1. 根据“Youtube - 2D platformer”的教程，在_gm对象里构建一个静态函数KillBubble()用于销毁对象。
2. 根据C#编程规范，在Enemy类中增加事件event Death，在_gm对象里监听这个事件。

碰到了问题，更新player stats UI的代码其实还是很长的，又在很多地方要用到。因此毫无疑问需要把他组织成方法。这里又和“Youtube - 2D platformer”的教程有差异。教程中他用了一个单独的类来进行update，但我只是在Player类里面有这个方法。看后续项目的需求来决定是否要如此重构吧。

### 4.26 ###

重新分析了一遍TapTians的数值。专注新手体验，得到了几个结论

1. 以前的分析有一个很大的遗漏，忘记了它在每个stage里有10个怪。
2. 第一个时间节点，*15*秒。用户完成了第一次升级，前15秒在给用户传递点击造成伤害，点的越快伤害越高的概念。在第15秒，完成第一次升级，**DPS加倍，所需时间从2.9降低到了1.45**，是一个小高潮。
3. 第二个时间节点，*58*秒，这时候用户打了第二个BOSS，耗时5秒，掉落9个金币。这里有几个要点
	- 在第15秒至58秒的周期内，在给用户传递打怪掉金币-金币升级-打怪更快-更多的金币，这个基础的循环概念。
	- 在这个周期内，玩家如果按时升级，那么打每只怪的所需时间的趋势是越来越短的，1.5-1.4-1.2。**玩家感觉自己会玩了**，渐入佳境。
	- 打BOSS耗时5秒并不是很多，由于打BOSS玩家总是会按的更快一些，那就更短了。
	- 掉落9个金币视觉效果非常好。
	- 由于按的快，因此性价比就高。给玩家初步传递了打BOSS有好康这一个概念。
4. 第三个时间节点，*77*秒。第三个BOSS，8秒17个金币，而之前只有2个，能连升2级，强化了打BOSS有好康这一个概念。
5. 第四个时间节点，*100*秒。第四个BOSS，11秒39个金币
	- 经过之前的训练，用户会知道在打BOSS时要升级，所以体验被固化。
	- 经过之前的概念传递，用户知道了打BOSS有好康，因此愿意试一试。
	- 打完39个金币离解锁新系统（自动打怪）所需的50个已近很近了！
	- 如果玩家愿意继续打，他会发现接下来一次有6个，和之前的1，2，4个比是质的提升。而且还是秒杀。
6. 第五个时间节点，*160*秒。从这里开始杀怪要2秒了。
7. 第六个时间节点，*230*秒。第10关，需要20秒，掉落412个金币。用户会很偏向于升级挂机产出。

经过这些分析，还有一种感觉，Hunting Souls解锁的第一个系统也应该是挂机。

感觉现在的泡泡生产算法有一些问题，如果popInterval长，会没的点；如果短，又很急躁。尝试这么改：

1. 是否要新生成泡泡和现在场上的泡泡数量有关，泡泡越多则下一个泡泡出来的越慢，但还是有一些随机性的。
2. 泡泡不会自动消失。
3. 每次点击消掉一个都会在0.2秒后新生成一个。（这样泡泡越来越多，也有问题）
4. 让泡泡的位置不要重叠。

理想状态是用户一直点，那么泡泡的数量维持在一个稳定的状态；一小会不点，泡泡立刻多了起来；一直不点，可以攒一波，但也别太多

### 4.27 ###
泡泡算法继续展开

1. 0-3个非常快，几乎不可能到0-1个。
2. 3-5个是比较舒适的区间
3. 5-10个依次变慢

这样只需要出现的间隔时间是泡泡数量有关的数组就行，搞定了，很舒服。

脑洞了几个技能

1. 掏蛋术：被动，几率出现伤害特别大的泡泡
2. 掏蛋术：主动，主动造成一次特别大的伤害
3. 气刃：不需要点泡泡才能造成伤害
4. 居合斩：攻击5次的影分身，点击造成伤害可以缩短CD，和弱点识破连击
5. 疯狂掏蛋术：所有攻击都变成掏蛋术的效果
6. 弱点识破：所有泡泡都变成掏蛋术的效果

代码方面把TapTitan的数值放到了游戏里面，处理了一下数值初始化的问题，由于估计点固定的位置要比乱点慢4倍，所以将血量除了4，感觉还是有点慢，可能除5差不多。

数值方面还遇到另外一个问题，升级点击所需的金币不好拟合，蛋疼。
昨天说感觉要先解锁挂机，但我对游戏的设想是挂机获得的产物不是金币而是材料，这样就有问题了，第一个解锁什么需要再考虑一下.

### 5.2 ###
参考了一下Deep town。挂机产品是材料没有什么大问题。不过是材料的话，那么金币的基本曲线不可能像Tap Titans一样那么抖。他那么抖的好处是无论玩家选择升级点击还是升级挂机，体验基本是一样的。选了材料，就要把玩家的抉择考虑进去，在适当的地方采用引导。

那么先实现物品箱。

物品箱在主界面提供入口，点击后，打开物品箱界面，**暂停主界面游戏**。（可选项）在入口下方，提供浮动文字展示。
点击打开物品箱界面后，每一个物品占据一个框体。框体标注图标，（待定项）名称，数量，（可选项）品质，(可选项）价格。（可选项）按品质分类，有品质TAG作为分类标题。
出售界面是弹窗还是同界面操作？同界面操作的设计难点在于如何设计重复信息的出现才既简介又明确。再找找参考。

又想了一下，还要实现浮动战斗文字，物品箱可能要考虑加载模式是用scene还是别的东西，找点参考工程看看。

### 5.4 ###
 
尝试了一下战斗文字用美术字，遇到了一些坑。标准的实现方式是把美术字当做一个Font。我则采用了就把他当做单纯的图片的方式，这样做直接导致的问题是位置关系比较难调。除此之外，测试了各种状态下不同生成物体方法的不同效果。

1.	类中声明GameObject，不赋值，直接Instantiate.-报错，UnassignedReferenceException.
2.	继承1，在Editor里拖一个Prefab进去.-正常生成，是标准方法。
3.	继承2，给Instantiate()方法传一个parent参数进去.-正常生成，Hierarchy层级正确.
4.	继承1，给Instantiate()方法传一个transform参数进去.-正常生成，transform正确.
5.	继承1，在Instantiate()之前更改transform.-正常生成，transform更改生效.
6.	继承5，给Instantiate()方法传一个transform参数进去.-正常生成，transform更改以Instantiate参数为准。
7.	直接new个空.-正常生成，Hierarchy出现New Game Object.
8.	直接new个数组.-**没有生成**.
9.	new个数组，再循环new个空.-正常生成，这么做显得很蠢。但实际用了这个方法...

基本搞定，剩下一点小玩意就搞定战斗文字了。

### 5.6 ###
实现了战斗文字。

替换了各种素材。

给气泡加入了简单的动画。

物品栏实现了一半功能。

实现物品栏时遇到了两个问题，一，物品数据的加载。因此去研究了一下直接加载Excel，了解了EPPlus这玩意，但没有深入了解，毕竟实现功能优先，最后还是直接加载txt。二，脚本控制动态UI生成，这里问题主要是位置，大小关系。做一系列测试记录一下。

1.	直接移动GameObject+Sprite.-移动到左上角后，position = (-2.67,4.85,0)。x,y的值和格子的坐标系是一样的。
2.	继承1，更改原素材的Pixels per Unit，从100改到1.-图片变的巨大。这个值的设定和预料的基本一致。
3.	unity的**默认大小会设置高为5个unit。**
4.	在(0,5,0)位置创建一个parent。把1中的GameObject拖进去。-.Editor中，该物品的Transform-position自动变成local position,该物品world position不变。
5.	继承4，拖出来.-Transform-position自动变成world position.
6.	继承4，parent进行缩放.-child相对位置不变，绝对位置发生了变化.
7.	继承6，将child拖出来.-发现缩放变成了绝对值。

### 5.11 ###
之前沉迷塞尔达加工作繁忙，今天继续测试。

1.	标准Instantiate()-.Position和Prefab一致.
2.	更改position再Instantiate().-更改生效.**Assets里Prefab的参数被改了！！**
3.	new 一个empty，Editor加载sprite.-ok
4.	new 一个empty，Resourcec.Load()加载sprite.-加载后强转，用Resourcec.Load<Sprite>()后成功。发现sprite中属性生效。

Give them a try:

- **Bold** (`Ctrl+B`) and *Italic* (`Ctrl+I`)
- Quotes (`Ctrl+Q`)
- Code blocks (`Ctrl+K`)
- Headings 1, 2, 3 (`Ctrl+1`, `Ctrl+2`, `Ctrl+3`)
- Lists (`Ctrl+U` and `Ctrl+Shift+O`)

### See your changes instantly with LivePreview ###

Don't guess if your [hyperlink syntax](http://markdownpad.com) is correct; LivePreview will show you exactly what your document looks like every time you press a key.

### Make it your own ###

Fonts, color schemes, layouts and stylesheets are all 100% customizable so you can turn MarkdownPad into your perfect editor.

### A robust editor for advanced Markdown users ###

MarkdownPad supports multiple Markdown processing engines, including standard Markdown, Markdown Extra (with Table support) and GitHub Flavored Markdown.

With a tabbed document interface, PDF export, a built-in image uploader, session management, spell check, auto-save, syntax highlighting and a built-in CSS management interface, there's no limit to what you can do with MarkdownPad.

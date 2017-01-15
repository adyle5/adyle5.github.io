---
layout:     post
title:      Columns in Jekyll
date:       2017-01-15 11:18:00
summary:    How to show two columns side by side on a Jekyll markdown page.
categories: jekyll markdown
---

On my about page, I wanted to display image and description side-by-side. On an html page you can do it easily using two divs, but this is a Jekyll markdown page and I still wanted to be able to use markdown syntax for most of the page. After searching online, I realized that no one had posted a solution. As it turns out, it was pretty simple to do with a combination of an html snippet and markdown.

Kramdown, the flavor of markdown supported by Jekyll out of the box, you have the option of combining html markup and markdown.

The solution turned out to be pretty simple. I put my image in a div with the width set and float: left. It put the image on the left. Then my text description was written in markdown. The engine puts the description in a <p> tag that starts to the left of the image and wraps around it. Pretty simple.

  {% highlight ruby %}
  <!-- image on the left -->
  <div style="width: 40%; float: left; display: block;">
    <img src="/images/pic.png" alt="Image" />
  </div>

  <!-- markdown on right -->
  ## header
  This is is the markdown text with a [link](http://link.com)
  {% endhighlight %}

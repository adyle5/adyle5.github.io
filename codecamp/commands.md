---
layout:     page
title:      Commands
date:       2017-03-05 10:14:00
summary:    Commands used during the presentation
categories: code-camp
permalink: /code-camp/commands/
---

### This is a sequential list of the commands we will be entering into the terminal during the presentation and websites were we will visit to get installers and other things. The quickest way for you to follow along with the presentation and to avoid errors, copy and paste the commands into the terminal and click on the links to the websites.

1. sudo apt-get updates
2. sudo apt-get upgrade
3. sudo apt-get install git
4. git --version
5. which git
6. [https://www.microsoft.com/net/core#linuxubuntu](https://www.microsoft.com/net/core#linuxubuntu){:target="_blank"}
7. For 14.04: sudo sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ trusty main" > /etc/apt/sources.list.d/dotnetdev.list'
8. For 16.04: sudo sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
9. sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893
10. sudo apt-get update
 [https://github.com/dotnet/core/tree/master/release-notes](https://github.com/dotnet/core/tree/master/release-notes){:target="_blank"}
12. sudo apt-cache search dotnet
13. sudo apt-get install dotnet-dev-1.0.0-rc4-004771
14. mkdir HelloWorldApp
15. cd HelloWorldApp
16. dotnet new
17. ls
18. sudo nano Program.cs (ctrl+x to exit)
19. dotnet restore
20. dotnet run
21. dotnet help
22. mkdir HelloAspNet
23. cd HelloAspNet
24. dotnet new mvc
25. dotnet restore
26. dotnet run
27. [http://localhost:5000](http://localhost:5000){:target="_blank"}
28. sudo apt-get install build-essential libssl-dev curl
29. [https://github.com/creationix/nvm](https://github.com/creationix/nvm){:target="_blank"}
30. curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.0/install.sh \| bash
31. command -v nvm
32. nvm install stable
33. npm install -g yo
34. npm install -g bower
35. npm install generator-aspnet
36. yo aspnet (select Web app basic, bootstrap, and give it a name)
37. cd <name>
38. dotnet restore
39. dotnet run
40. git clone https://github.com/adyle5/code_camp_template.git/
41. cd code_camp_template
42. dotnet restore
43. dotnet run
44. cp -r ~/code_camp_template/* ~/HelloCodeCamp/
45. cd HelloCodeCamp
46. ls -a
47. (if .git exists: rm -rf .git)
48. [https://code.visualstudio.com/](https://code.visualstudio.com/){:target="_blank"}
49. cd Downloads
50. ls
51. sudo dpkg -i code (then tab)
52. cd ~/HelloCodeCamp
53. code .
54. git remote add origin <url>
55. git push origin master

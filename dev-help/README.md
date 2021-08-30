# How to use GitHub & Git

## Table of Contents

 1. [What is Git?](#what-is-git)
 2. [Installation](#installation)
 3. [Important Commands](#important-commands)
 4. [Project Sequence](#project-sequence)

## What is Git?

Git is an open source version control system that allows a development team to manage changes to documents, programs, web sites, and other collections of information. GitHub hosts multiple Git repositories and is a great tool to connect teams and showcase a code portfolio.

## Installation

To use Git in its purest form, we recommend using Git Bash. Git Bash is a Linux-feeling command line interface that allows you to work with Git commands at the lowest level (little to no abstraction). Working with Git in this way allows you to have complete understanding of how your work is managed.

To install Git Bash visit [this link](https://gitforwindows.org/) and download the version appropriate for your machine's operating system.

## Important Commands

Understanding the following commands is vital for ensuring correct sequencing and saving of data. While these commands are listed in bulk here, please reference the Project Sequence to see the right way to use these commands.

### Clone
Cloning is what enables you to take an online repo and copy it to your local machine. The changes you make on your local machine are not official (as far as the rest of the team is concerned) until you add, commit, and push these changes.

    git clone <HTTPS or SSH>

### Add
A simple add command tells Git, "hey I made changes to these files, please track them." This is an important step because if you make any new files, Git doesn't know if you want them included in the project. This all-inclusive or explicit command allows you to do just that. 

The first command will automatically tell Git to track all modifications that you have made (a .gitignore file filters out any extraneous files that are not important to the project, think temp files). The second allows you to specify a specific file to track.

    git add *
    git add <file>

### Commit
A commit is essentially the "save" command for repo things. All files that you change (and want on the online repo) need to be staged and saved to a hidden folder in your local repo (you can find it if you look for .git directory). When you run this command, Git will basically copy all your modified files into this directory preparing to send them off to the online repo.

All commits need to specify a SHORT commit message (commit title) that answers two questions, 1) Why does this file need to be added, and 2) What is in this commit (what files/functionality)? Optionally, you can add a more verbose description following the first message.

    git commit -m "Commit Title" -m "Commit Description"

### Push
This step takes the contents of your .git directory and sends it off to the online repository. Essentially, your changes can now be seen, downloaded, and modified by the rest of the team.

The first command must be run if this is the first time you are on a new branch (or on a new repo). It specifies the up-stream, or where you are committing all of these changes to. Once you have run this command, you can just simply enter "git push." If you forget, don't worry, Git will politely tell you it does not know what the up-stream and will list the command it needs you to run.

    git push -u origin <branch>
    git push

### Pull
A pull is the opposite of a push. Instead of sending changes to the repo, you are downloading updates from the repo. If your friend just "pushed" his/her changes, you can "pull" to see everything he/she just worked on. Similar to push, you may need to enter the first command when working with a new branch. After that it is just "git pull."

    git pull origin master
    git pull

### Branch
Branches are the heart of team development. We will create a new branch (or multiple branches) for each pull request we have. A pull request is essentially a specific, narrow functionality. Say if we have an website, one pull-request/branch may be dedicated to creating a feedback form. A branch basically stems off of the live-application (or main app) and lets you work in a sandbox environment without disrupting the code that is running for users or other purposes.

The following command is used to tell a user what branch they are currently working on.

    git branch

The next two commands are used to switch between different branches. The main branch (or if you watch Loki, the Sacred Timeline) is called "master." Any other branch names are up to the team's discretion. The first command allows you to create a branch with whatever name you choose. After running, Git creates this new branch and automatically puts you in this working branch (if you run git branch, Git will also tell you that you switched into this new branch). The second command is used to switch between existing branches.

    git checkout -b <branch>
    git checkout <branch>

When working with branches it is important to see what has changed since you, well, branched. The following command will show the difference between your current branch and its parent. So for example,  if I branched off of master, running git diff would tell me the files and line numbers of those files that have been modified, deleted, or created.

    git diff

## Project Sequence
The following sequence/practice is what we will do to ensure our development stays organized.

1. Create a new branch (sprintx-
2. Modify, create, and delete files within this branch
3. Create and link a pull request to this branch (the name should be relatively the same to the branch)
4. Review the pull request
	* Provide reviews, comments, and verification to the pull request
	* Get the pull request approved by a project manager
5. Project manager approves request
6. Branch is merged to parent branch
7. Delete branch/pull request once merge is successful
8. Start all over

I recommend using GitHub Desktop to manage pull requests.

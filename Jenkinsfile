pipeline {
    agent any
    stages {
        stage('Checkout Code') {
            steps {
                git branch: 'main', url: 'https://github.com/Niruifedi/Personalized_Library_System.git'
            }
        }
        stage('Code Analysis') {
            environment {
                scannerHome = tool name: 'Personalized_Library'
            }
            steps {
                script {
                    withSonarQubeEnv('sonar') {
                        sh """
                            ${scannerHome}/bin/sonar-scanner \
                                -Dsonar.projectKey=Personalized_Library \
                                -Dsonar.projectName=Personalized_Library \
                                -Dsonar.sources=.
                        """
                    }
                }
            }
        }
    }
}

pipeline 
{
    agent any
    stages 
	{
        stage('build') 
		{
            steps 
			{
                withMaven(maven: 'maven_3_5_0')
				{
					sh 'mvn clean compile'
				}
            }
        }

		stage('deploy') 
		{
			steps 
			{
				withMaven(maven: 'maven_3_5_0')
				{
					sh 'mvn deploy'
				}
			}
			
		}
	}
}
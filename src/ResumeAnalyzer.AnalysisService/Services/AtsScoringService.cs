namespace ResumeAnalyzer.AnalysisService.Services
{
    using ResumeAnalyzer.AnalysisService.Interfaces;
    using ResumeAnalyzer.Shared.Models;

    public class AtsScoringService : IAtsScoringService
    {
        private readonly List<string> _importantSkills =
        [
                            // Languages
                            "C#",
                            ".NET",
                            "ASP.NET",
                            "ASP.NET Core",
                            "VB.NET",
                            "F#",
                            "Java",
                            "Kotlin",
                            "Scala",
                            "Python",
                            "JavaScript",
                            "TypeScript",
                            "Go",
                            "Rust",
                            "C",
                            "C++",
                            "PHP",
                            "Ruby",
                            "Swift",
                            "Objective-C",
                            "Dart",
                            "R",
                            "PowerShell",
                            "Bash",

                            // Frontend
                            "HTML",
                            "HTML5",
                            "CSS",
                            "CSS3",
                            "Sass",
                            "SCSS",
                            "Less",
                            "Bootstrap",
                            "Tailwind CSS",
                            "React",
                            "Next.js",
                            "Angular",
                            "Vue.js",
                            "Nuxt.js",
                            "Svelte",
                            "Redux",
                            "jQuery",
                            "Blazor",
                            "Webpack",
                            "Vite",

                            // Backend
                            "ASP.NET Web API",
                            "Minimal APIs",
                            "Node.js",
                            "Express",
                            "NestJS",
                            "Spring Boot",
                            "Hibernate",
                            "Entity Framework",
                            "Entity Framework Core",
                            "Django",
                            "Flask",
                            "FastAPI",
                            "Laravel",
                            "Symfony",
                            "Ruby on Rails",

                            // Databases
                            "SQL",
                            "T-SQL",
                            "SQL Server",
                            "Azure SQL",
                            "PostgreSQL",
                            "MySQL",
                            "MariaDB",
                            "Oracle",
                            "SQLite",
                            "MongoDB",
                            "Cosmos DB",
                            "Redis",
                            "Cassandra",
                            "DynamoDB",
                            "Elasticsearch",

                            // Cloud
                            "Azure",
                            "AWS",
                            "Google Cloud",
                            "Azure Functions",
                            "Azure App Service",
                            "Azure Storage",
                            "Azure Service Bus",
                            "Azure Event Grid",
                            "Azure Event Hubs",
                            "Azure Key Vault",
                            "Azure API Management",
                            "Azure DevOps",
                            "Azure Container Apps",
                            "Azure Kubernetes Service",
                            "AWS Lambda",
                            "EC2",
                            "S3",
                            "RDS",
                            "CloudFormation",

                            // Containers & DevOps
                            "Docker",
                            "Docker Compose",
                            "Kubernetes",
                            "Helm",
                            "Terraform",
                            "Ansible",
                            "Git",
                            "GitHub",
                            "GitLab",
                            "CI/CD",
                            "GitHub Actions",
                            "Azure Pipelines",
                            "Jenkins",
                            "TeamCity",
                            "Octopus Deploy",

                            // Architecture
                            "Microservices",
                            "REST",
                            "REST API",
                            "GraphQL",
                            "gRPC",
                            "SOAP",
                            "Event Driven Architecture",
                            "CQRS",
                            "MediatR",
                            "Domain Driven Design",
                            "Clean Architecture",
                            "Onion Architecture",
                            "Hexagonal Architecture",
                            "SOLID",
                            "Design Patterns",

                            // Messaging
                            "RabbitMQ",
                            "Apache Kafka",
                            "Azure Service Bus",
                            "MassTransit",
                            "NServiceBus",

                            // Testing
                            "Unit Testing",
                            "Integration Testing",
                            "End-to-End Testing",
                            "xUnit",
                            "NUnit",
                            "MSTest",
                            "Moq",
                            "FluentAssertions",
                            "SpecFlow",
                            "Playwright",
                            "Selenium",
                            "Cypress",

                            // Security
                            "OAuth",
                            "OAuth2",
                            "OpenID Connect",
                            "JWT",
                            "IdentityServer",
                            "Microsoft Entra ID",
                            "Azure Active Directory",

                            // APIs
                            "OpenAPI",
                            "Swagger",
                            "SignalR",
                            "WebSockets",

                            // Monitoring
                            "Application Insights",
                            "Serilog",
                            "Seq",
                            "Prometheus",
                            "Grafana",
                            "OpenTelemetry",
                            "ELK Stack",

                            // Build Tools
                            "MSBuild",
                            "Maven",
                            "Gradle",
                            "npm",
                            "pnpm",
                            "Yarn",

                            // Methodologies
                            "Agile",
                            "Scrum",
                            "Kanban",
                            "DevOps",
                            "TDD",
                            "BDD"
                        ];

        public Task<AtsScoreBreakdown> CalculateAsync(string resumeText)
        {
            var score = 0;

            var foundSkills = new List<string>();
            var missingSkills = new List<string>();

            foreach (var skill in _importantSkills)
            {
                if (resumeText.Contains(skill, StringComparison.OrdinalIgnoreCase))
                {
                    score += 10;
                    foundSkills.Add(skill);
                }
                else
                {
                    missingSkills.Add(skill);
                }
            }

            return Task.FromResult(new AtsScoreBreakdown
            {
                TotalScore = score,
                MissingSkills = missingSkills,
                Recommendations =
                                        [
                                            "Add quantified achievements",
                                        "Highlight cloud projects"
                                        ]
            });
        }
    }
}

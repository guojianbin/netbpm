﻿using NetBpm.Util.DB;
using NetBpm.Util.EComp;
using NetBpm.Util.Xml;
using NetBpm.Workflow.Definition.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTest
{
    [TestFixture]
    public class ProcessDefinitionImplTest
    {
        public XmlElement helloWorld1() 
        {
            /*
             <?xml version="1.0"?>

            <process-definition>

              <name>Hello world 1</name>
              <description>This is the simples process.</description>

              <start-state name="start">
                <transition to="first activity state" />
              </start-state>

              <end-state name="end" />

              <activity-state name="first activity state">
                <description>this is the first state</description>
                <assignment handler="NetBpm.Workflow.Delegation.Impl.Assignment.AssignmentExpressionResolver, NetBpm">
                  <parameter name="expression">processInitiator</parameter>
                </assignment>
                <transition to="end" />
              </activity-state>

            </process-definition>
             */
            #region create Xml
            XmlElement xmlElement = new XmlElement("process-definition");
            XmlElement nameElement = new XmlElement("name");
            nameElement.Content.Add("Hello world 1");
            XmlElement descriptionElement = new XmlElement("description");
            descriptionElement.Content.Add("This is the simples process.");
            XmlElement startStateElement = new XmlElement("start-state");
            startStateElement.Attributes.Add("name", "start");
            XmlElement transitionElement = new XmlElement("transition");
            transitionElement.Attributes.Add("to", "first activity state");
            startStateElement.AddChild(transitionElement);
            XmlElement endStateElement = new XmlElement("end-state");
            endStateElement.Attributes.Add("name", "end");

            XmlElement activityStateElement = new XmlElement("activity-state");
            activityStateElement.Attributes.Add("name", "first activity state");
            XmlElement activityStateDescriptionElement = new XmlElement("description");
            activityStateDescriptionElement.Content.Add("this is the first state");
            XmlElement assignmentElement = new XmlElement("assignment");
            assignmentElement.Attributes.Add("handler", "NetBpm.Workflow.Delegation.Impl.Assignment.AssignmentExpressionResolver, NetBpm");
            XmlElement parameterElement = new XmlElement("parameter");
            parameterElement.Attributes.Add("name", "expression");
            parameterElement.Content.Add("processInitiator");
            assignmentElement.AddChild(parameterElement);
            XmlElement activityStateTransitionElement = new XmlElement("transition");
            activityStateTransitionElement.Attributes.Add("to", "end");
            activityStateElement.AddChild(activityStateDescriptionElement);
            activityStateElement.AddChild(assignmentElement);
            activityStateElement.AddChild(activityStateTransitionElement);

            xmlElement.AddChild(nameElement);
            xmlElement.AddChild(descriptionElement);
            xmlElement.AddChild(startStateElement);
            xmlElement.AddChild(endStateElement);
            xmlElement.AddChild(activityStateElement);
            #endregion
            return xmlElement;
        }

        [Test]
        public void ReadProcessDataTest() 
        {
            XmlElement xmlElement = helloWorld1();
            ProcessDefinitionImpl processDefinition = new ProcessDefinitionImpl();
            ProcessDefinitionBuildContext processDefinitionBuilder = new ProcessDefinitionBuildContext(processDefinition, null,  null);
            processDefinition.ReadProcessData(xmlElement, processDefinitionBuilder);

            Assert.AreEqual("Hello world 1", processDefinition.Name);
            Assert.AreEqual("This is the simples process.", processDefinition.Description);
            Assert.IsNotNull(processDefinition.StartState);
            Assert.AreEqual("start",processDefinition.StartState.Name);
            Assert.IsNotNull(processDefinition.EndState);
            Assert.AreEqual("end",processDefinition.EndState.Name);
            //activity-state在Nodes裏面，以後再換掉
        }
    }
}

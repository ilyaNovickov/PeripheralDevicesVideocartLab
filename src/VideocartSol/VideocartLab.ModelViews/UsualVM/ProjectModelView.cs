using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public class ProjectModelView : ModelViewBase
    {
        private Type? candidatToAdd = null;
        private NodeFactoryService factoryService;

        #if DEBUG
        public ProjectModelView()
        {

        }
        #endif
        public ProjectModelView(NodeFactoryService factoryService)
        {
            this.factoryService = factoryService;
        }

        private NodeFactoryService NodeFactoryService { get { return factoryService; } }

        public Type? CandidateToAdd
        {
            get => candidatToAdd;
            set
            {
                candidatToAdd = value;
            }
        }
        private double x = 0d;
        private double y = 0d;

        public NodeModelView? AddNode()
        {
            if (CandidateToAdd == null)
                return null;

            var node = NodeFactoryService.CreateNode(CandidateToAdd, x, y);
            x += 100d;
            y += 100d;

            return node;
        }
    }
}

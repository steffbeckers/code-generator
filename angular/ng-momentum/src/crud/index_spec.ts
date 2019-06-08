import { Tree } from '@angular-devkit/schematics';
import { SchematicTestRunner, UnitTestTree } from '@angular-devkit/schematics/testing';
import * as path from 'path';
import { Schema as ApplicationOptions } from '@schematics/angular/application/schema';
import { Schema as WorkspaceOptions } from '@schematics/angular/workspace/schema';
import * as sinon from 'sinon';
import * as fetch from 'node-fetch';

// SchematicTestRunner needs an absolute path to the collection to test.
const collectionPath = path.join(__dirname, '../collection.json');

describe('crud', () => {
  const schematicRunner = new SchematicTestRunner(
    '@schematics/angular',
    path.join(__dirname, '..', '..', 'node_modules', '@schematics', 'angular', 'collection.json')
  );

  const customRunner = new SchematicTestRunner('momentum', path.join(__dirname, '..', 'collection.json'));

  const workspaceOptions: WorkspaceOptions = {
    name: 'workspace',
    newProjectRoot: 'projects',
    version: '6.0.0',
  };

  describe('with project', () => {
    const appOptions: ApplicationOptions = {
      name: 'bar',
      inlineStyle: false,
      inlineTemplate: false,
      routing: false,
      style: 'scss',
      skipTests: false,
      skipPackageJson: false,
    };

    let appTree: UnitTestTree;
    beforeEach(() => {
      appTree = schematicRunner.runSchematic('workspace', workspaceOptions);
      appTree = schematicRunner.runSchematic('application', appOptions, appTree);
      appTree = customRunner.runSchematic(
        'scaffold',
        {
          spec: true,
        },
        appTree
      );
    });

    it('requires required option', () => {
      // We test that
      const runner = new SchematicTestRunner('momentum', collectionPath);
      expect(() => runner.runSchematic('crud', {}, appTree)).toThrow();
    });

    it('fails without app tree', () => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      expect(() => runner.runSchematic('crud', {}, Tree.empty())).toThrow();
    });

    it('works', () => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      const tree = runner.runSchematic(
        'crud',
        {
          name: 'test',
        },
        appTree
      );

      // Listing files
      expect(tree.files.indexOf('/projects/bar/src/app/vos/test/test.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/vos/test/test.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/projects/bar/src/app/services/tests/tests.service.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/services/tests/tests.service.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(
        tree.readContent('/projects/bar/src/app/services/tests/tests.service.ts').indexOf("import { Test } from 'src/app/vos/test/test';")
      ).toBeGreaterThanOrEqual(0);

      expect(
        tree.readContent('/projects/bar/src/app/services/tests/tests.service.spec.ts').indexOf("import { Test } from 'src/app/vos/test/test';")
      ).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.spec.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.guard.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.spec.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.guard.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.spec.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.guard.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.spec.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.guard.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.module.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.routing.module.ts')).toBeGreaterThanOrEqual(0);
    });

    it('works without tests', () => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      const tree = runner.runSchematic(
        'crud',
        {
          name: 'test',
          spec: false,
        },
        appTree
      );

      // Listing files
      expect(tree.files.indexOf('/projects/bar/src/app/vos/test/test.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/vos/test/test.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/projects/bar/src/app/services/tests/tests.service.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/services/tests/tests.service.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.spec.ts')).toBe(-1);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.guard.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.spec.ts')).toBe(-1);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.guard.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.spec.ts')).toBe(-1);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.guard.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.spec.ts')).toBe(-1);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.guard.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.module.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.routing.module.ts')).toBeGreaterThanOrEqual(0);
    });
  });

  describe('without project', () => {
    const appOptions: ApplicationOptions = {
      name: 'bar',
      projectRoot: '',
      inlineStyle: false,
      inlineTemplate: false,
      routing: false,
      style: 'scss',
      skipTests: false,
      skipPackageJson: false,
    };

    let appTree: UnitTestTree;
    beforeEach(() => {
      appTree = schematicRunner.runSchematic('workspace', workspaceOptions);
      appTree = schematicRunner.runSchematic('application', appOptions, appTree);
      appTree = customRunner.runSchematic(
        'scaffold',
        {
          spec: true,
        },
        appTree
      );
    });

    it('requires required option', () => {
      // We test that
      const runner = new SchematicTestRunner('momentum', collectionPath);
      expect(() => runner.runSchematic('crud', {}, appTree)).toThrow();
    });

    it('fails without app tree', () => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      expect(() => runner.runSchematic('crud', {}, Tree.empty())).toThrow();
    });

    it('works', () => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      const tree = runner.runSchematic(
        'crud',
        {
          name: 'test',
        },
        appTree
      );

      // Listing files
      expect(tree.files.indexOf('/src/app/vos/test/test.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/vos/test/test.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/src/app/services/tests/tests.service.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/services/tests/tests.service.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/src/app/views/test/test.edit.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.component.spec.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.guard.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/src/app/views/test/tests.list.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.component.spec.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.guard.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/src/app/views/test/test.new.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.component.spec.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.guard.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/src/app/views/test/test.details.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.component.spec.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.guard.spec.ts')).toBeGreaterThanOrEqual(0);

      expect(tree.files.indexOf('/src/app/views/test/test.module.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.routing.module.ts')).toBeGreaterThanOrEqual(0);
    });

    it('works without tests', () => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      const tree = runner.runSchematic(
        'crud',
        {
          name: 'test',
          spec: false,
        },
        appTree
      );

      // Listing files
      expect(tree.files.indexOf('/src/app/vos/test/test.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/vos/test/test.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/src/app/services/tests/tests.service.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/services/tests/tests.service.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/src/app/views/test/test.edit.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.component.spec.ts')).toBe(-1);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.edit.guard.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/src/app/views/test/tests.list.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.component.spec.ts')).toBe(-1);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/tests.list.guard.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/src/app/views/test/test.new.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.component.spec.ts')).toBe(-1);
      expect(tree.files.indexOf('/src/app/views/test/test.new.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.new.guard.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/src/app/views/test/test.details.component.scss')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.component.html')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.component.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.component.spec.ts')).toBe(-1);
      expect(tree.files.indexOf('/src/app/views/test/test.details.guard.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.details.guard.spec.ts')).toBe(-1);

      expect(tree.files.indexOf('/src/app/views/test/test.module.ts')).toBeGreaterThanOrEqual(0);
      expect(tree.files.indexOf('/src/app/views/test/test.routing.module.ts')).toBeGreaterThanOrEqual(0);
    });
  });

  describe('with url and single object', () => {
    const appOptions: ApplicationOptions = {
      name: 'bar',
      inlineStyle: false,
      inlineTemplate: false,
      routing: false,
      style: 'scss',
      skipTests: false,
      skipPackageJson: false,
    };

    let appTree: UnitTestTree, sandbox;
    beforeEach(() => {
      appTree = schematicRunner.runSchematic('workspace', workspaceOptions);
      appTree = schematicRunner.runSchematic('application', appOptions, appTree);
      appTree = customRunner.runSchematic(
        'scaffold',
        {
          spec: true,
          style: 'scss',
        },
        appTree
      );
      sandbox = sinon.createSandbox();
      sandbox.stub(fetch, 'Promise').returns(
        Promise.resolve({
          json: function() {
            return { a: '1', b: 2, c: true };
          },
        })
      );
    });

    afterEach(() => {
      sandbox.restore();
    });

    it('requires required option', () => {
      // We test that
      const runner = new SchematicTestRunner('momentum', collectionPath);
      expect(() => runner.runSchematic('crud', {}, appTree)).toThrow();
    });

    it('fails without app tree', () => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      expect(() => runner.runSchematic('crud', {}, Tree.empty())).toThrow();
    });

    it('works', (done) => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      runner
        .runSchematicAsync(
          'crud',
          {
            name: 'test',
            url: 'http://localhost:8080/my-endpoint/',
          },
          appTree
        )
        .subscribe((tree) => {
          // Listing files
          expect(tree.files.indexOf('/projects/bar/src/app/vos/test/test.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/vos/test/test.spec.ts')).toBeGreaterThanOrEqual(0);

          expect(tree.files.indexOf('/projects/bar/src/app/services/tests/tests.service.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/services/tests/tests.service.spec.ts')).toBeGreaterThanOrEqual(0);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.scss')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.html')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.spec.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.guard.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.guard.spec.ts')).toBeGreaterThanOrEqual(0);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.scss')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.html')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.spec.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.guard.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.guard.spec.ts')).toBeGreaterThanOrEqual(0);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.scss')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.html')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.spec.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.guard.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.guard.spec.ts')).toBeGreaterThanOrEqual(0);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.scss')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.html')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.spec.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.guard.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.guard.spec.ts')).toBeGreaterThanOrEqual(0);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.module.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.routing.module.ts')).toBeGreaterThanOrEqual(0);
          done();
        });
    });

    it('works without tests', (done) => {
      const runner = new SchematicTestRunner('momentum', collectionPath);
      runner
        .runSchematicAsync(
          'crud',
          {
            name: 'test',
            spec: false,
            url: 'http://localhost:8080/my-endpoint/',
          },
          appTree
        )
        .subscribe((tree) => {
          // Listing files
          expect(tree.files.indexOf('/projects/bar/src/app/vos/test/test.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/vos/test/test.spec.ts')).toBe(-1);

          expect(tree.files.indexOf('/projects/bar/src/app/services/tests/tests.service.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/services/tests/tests.service.spec.ts')).toBe(-1);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.scss')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.html')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.component.spec.ts')).toBe(-1);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.guard.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.edit.guard.spec.ts')).toBe(-1);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.scss')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.html')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.component.spec.ts')).toBe(-1);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.guard.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/tests.list.guard.spec.ts')).toBe(-1);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.scss')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.html')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.component.spec.ts')).toBe(-1);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.guard.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.new.guard.spec.ts')).toBe(-1);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.scss')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.html')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.component.spec.ts')).toBe(-1);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.guard.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.details.guard.spec.ts')).toBe(-1);

          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.module.ts')).toBeGreaterThanOrEqual(0);
          expect(tree.files.indexOf('/projects/bar/src/app/views/test/test.routing.module.ts')).toBeGreaterThanOrEqual(0);
          done();
        });
    });
  });
});

import { Component, ElementRef, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import * as THREE from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';
import { RoomEnvironment } from 'three/examples/jsm/environments/RoomEnvironment.js';
import { PMREMGenerator } from 'three/src/extras/PMREMGenerator.js';
import gsap from 'gsap';

@Component({
  selector: 'app-atm-scene',
  templateUrl: './atm-scene.html',
  styleUrls: ['./atm-scene.css']
})
export class AtmSceneComponent implements AfterViewInit, OnDestroy {
  @ViewChild('rendererCanvas') rendererCanvas!: ElementRef<HTMLCanvasElement>;

  private renderer!: THREE.WebGLRenderer;
  private scene!: THREE.Scene;
  private camera!: THREE.PerspectiveCamera;
  private controls!: any;
  private animationId!: number;
  private raycaster = new THREE.Raycaster();
  private mouse = new THREE.Vector2();
  public isInterfaceActive = false;

  constructor() { }

  private onMouseClick(event: MouseEvent): void {
    // Si la interfaz ya está abierta, no hacemos nada (para no volver a animar)
    if (this.isInterfaceActive) return;

    const canvas = this.rendererCanvas.nativeElement;
    const rect = canvas.getBoundingClientRect();

    // Calcular posición del mouse normalizada (-1 a +1)
    this.mouse.x = ((event.clientX - rect.left) / rect.width) * 2 - 1;
    this.mouse.y = -((event.clientY - rect.top) / rect.height) * 2 + 1;

    // Disparar rayo
    this.raycaster.setFromCamera(this.mouse, this.camera);
    const intersects = this.raycaster.intersectObjects(this.scene.children, true);

    if (intersects.length > 0) {
      // Si tocamos cualquier parte del modelo, hacemos foco
      console.log('Click detectado en el cajero');
      this.focusOnScreen();
    }
  }

  private focusOnScreen(): void {
    // Coordenadas OBJETIVO (A donde mira la cámara)
    // Ajusta la 'y' para subir/bajar la mirada a la pantalla
    const targetPoint = new THREE.Vector3(0, 1.3, 0); 

    // Coordenadas DE LA CÁMARA (Donde se coloca la cámara)
    // Ajusta la 'z' para acercarte más o menos
    const cameraPoint = new THREE.Vector3(0, 1.3, 0.8); 

    // Desactivamos control manual
    this.controls.enabled = false;

    // Animar posición de la cámara
    gsap.to(this.camera.position, {
      x: cameraPoint.x,
      y: cameraPoint.y,
      z: cameraPoint.z,
      duration: 1.5,
      ease: 'power2.inOut'
    });

    // Animar hacia dónde mira la cámara
    gsap.to(this.controls.target, {
      x: targetPoint.x,
      y: targetPoint.y,
      z: targetPoint.z,
      duration: 1.5,
      ease: 'power2.inOut',
      onUpdate: () => this.controls.update(),
      onComplete: () => {
        // Al terminar, mostramos el HTML
        this.isInterfaceActive = true;
      }
    });
  }

  public exitAtm(): void {
    this.isInterfaceActive = false;
    this.controls.enabled = true; // Devolvemos el control al usuario

    // Regresamos la cámara a la posición original
    gsap.to(this.camera.position, {
      x: 0, 
      y: 1.5, 
      z: 5,
      duration: 1.2,
      ease: 'power2.inOut'
    });

    gsap.to(this.controls.target, {
      x: 0, y: 0, z: 0,
      duration: 1.2,
      onUpdate: () => this.controls.update()
    });
  }
  
  ngAfterViewInit(): void {
    this.initThree();
    this.loadModel();
    this.animate();
    
    // Ajustar si el usuario cambia el tamaño de la ventana
    this.rendererCanvas.nativeElement.addEventListener('click', (event) => this.onMouseClick(event));
    window.addEventListener('resize', () => this.onWindowResize());
  }
  
  private initThree(): void {
    const canvas = this.rendererCanvas.nativeElement;
    const width = canvas.parentElement?.clientWidth || window.innerWidth;
    const height = canvas.parentElement?.clientHeight || window.innerHeight;

    // 1. ESCENA Y FONDO
    this.scene = new THREE.Scene();
    this.scene.background = new THREE.Color(0x1a1a2e); // Color azul oscuro estilo "banco seguro"

    // 2. CÁMARA
    this.camera = new THREE.PerspectiveCamera(45, width / height, 0.1, 1000);
    this.camera.position.set(0, 1.5, 5); // Posición inicial: un poco arriba y atrás

    // 3. RENDERER
    this.renderer = new THREE.WebGLRenderer({ canvas, antialias: true, alpha: true });
    this.renderer.setSize(width, height);
    this.renderer.setPixelRatio(window.devicePixelRatio);
    this.renderer.shadowMap.enabled = true; // Activar sombras

    this.renderer.outputColorSpace = THREE.SRGBColorSpace;
    // Opcional: ajusta la exposición si se ve muy brillante u oscuro después
    this.renderer.toneMapping = THREE.ACESFilmicToneMapping;
    this.renderer.toneMappingExposure = 0.5;

    // 4. LUCES (Clave para que se vea bien)
    const ambientLight = new THREE.AmbientLight(0xffffff, 0.2); // <--- Estaba en 0.6
    this.scene.add(ambientLight);

    // Bajar la luz direccional (el sol)
    const directionalLight = new THREE.DirectionalLight(0xffffff, 0.5); // <--- Estaba en 1
    directionalLight.position.set(5, 10, 7);
    directionalLight.castShadow = true;
    this.scene.add(directionalLight);
    const pmremGenerator = new THREE.PMREMGenerator(this.renderer);

    pmremGenerator.compileEquirectangularShader();
    this.scene.environment = pmremGenerator.fromScene(new RoomEnvironment(), 0.04).texture;

    this.controls = new OrbitControls(this.camera, this.renderer.domElement);

    // 5. CONTROLES (Para rotar con el mouse mientras desarrollas)
    this.controls = new OrbitControls(this.camera, this.renderer.domElement);
    this.controls.enableDamping = true; // Movimiento suave
  }

  private loadModel(): void {
    const loader = new GLTFLoader();
    
    // Ruta del modelo GLB
    loader.load('/assets/models/cajero.glb', (gltf: any) => {
      const model = gltf.scene;
      
      // Ajustes comunes al modelo
      model.position.set(0, 0, 0); 
      model.scale.set(1, 1, 1); // Juega con esto si el modelo es muy grande o chico
      
      // Activar sombras en el modelo
      model.traverse((child: any) => {
        if (child.isMesh) {
          child.castShadow = true;
          child.receiveShadow = true;
        }
      });

      this.scene.add(model);
      console.log('Modelo cargado exitosamente');

    }, undefined, (error: any) => {
      console.error('Error cargando el modelo:', error);
    });
  }

  private animate(): void {
    // Loop de renderizado (se ejecuta 60 veces por segundo)
    this.animationId = requestAnimationFrame(() => this.animate());
    
    if (this.controls) this.controls.update();
    
    this.renderer.render(this.scene, this.camera);
  }

  private onWindowResize(): void {
    // Mantener la proporción al cambiar tamaño de ventana
    const width = window.innerWidth;
    const height = window.innerHeight;
    this.camera.aspect = width / height;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(width, height);
  }

  ngOnDestroy(): void {
    // Limpieza para evitar fugas de memoria
    if (this.animationId) cancelAnimationFrame(this.animationId);
  }
}